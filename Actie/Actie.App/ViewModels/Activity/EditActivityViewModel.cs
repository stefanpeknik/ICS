// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.Tokens;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class EditActivityViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityTagFacade _activityTagFacade;
    private readonly ITagFacade _tagFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StartDate))]
    [NotifyPropertyChangedFor(nameof(EndDate))]
    [NotifyPropertyChangedFor(nameof(StartTime))]
    [NotifyPropertyChangedFor(nameof(EndTime))]
    public ActivityDetailModel activity = ActivityDetailModel.Empty with { Start = DateTime.Now - TimeSpan.FromHours(1), End = DateTime.Now };

    [ObservableProperty]
    public IList<ProjectListModel> myProjects;

    [ObservableProperty]
    public ProjectListModel selectedProject;

    [ObservableProperty]
    public IList<TagListModel> tags;

    [ObservableProperty]
    public TagListModel? selectedTag;

    [ObservableProperty]
    public IList<TagListModel> selectedTags;

    public EditActivityViewModel(IActivityTagFacade activityTagFacade, ITagFacade tagFacade, IProjectFacade projectFacade, IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService) : base(messengerService)
    {
        _activityTagFacade = activityTagFacade;
        _tagFacade = tagFacade;
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);
        MyProjects = (await _projectFacade.GetByUserIdAsync(UserId)).ToList();
        SelectedTags = (await _tagFacade.GetTagsOfActivityAsync(Id)).ToList();

        // filters out all already added Tags
        Tags = (await _tagFacade.GetAsync()).Where(t => SelectedTags.All(st => st.Id != t.Id)).ToList();

        SelectedTag = Tags.FirstOrDefault();

        SelectedProject = MyProjects.First(p => p.Id == Activity.ProjectId);
    }
    public DateTime StartDate
    {
        get => Activity.Start;
        set
        {
            if (Activity.Start != value)
            {
                TimeSpan time = Activity.Start.TimeOfDay;
                Activity.Start = new DateTime(value.Year, value.Month, value.Day);
                Activity.Start = Activity.Start.Add(time);
                if (Activity.Start > Activity.End)
                    Activity.Start = Activity.End - TimeSpan.FromHours(1);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public DateTime EndDate
    {
        get => Activity.End;
        set
        {
            if (Activity.End != value)
            {
                TimeSpan time = Activity.End.TimeOfDay;
                Activity.End = new DateTime(value.Year, value.Month, value.Day);
                Activity.End = Activity.End.Add(time);
                if (Activity.End < Activity.Start)
                    Activity.End = Activity.Start + TimeSpan.FromHours(1);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public TimeSpan StartTime
    {
        get => Activity.Start.TimeOfDay;
        set
        {
            if (Activity.Start.TimeOfDay != value)
            {
                Activity.Start = new DateTime(Activity.Start.Year, Activity.Start.Month, Activity.Start.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public TimeSpan EndTime
    {
        get => Activity.End.TimeOfDay;
        set
        {
            if (Activity.End.TimeOfDay != value)
            {
                Activity.End = new DateTime(Activity.End.Year, Activity.End.Month, Activity.End.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }
    public bool CanSave => Activity.Name != string.Empty && Activity.Type != string.Empty && !MyProjects.IsNullOrEmpty() &&
                           (Activity.Start < Activity.End);


    [RelayCommand]
    public async Task AddTagAsync()
    {
        if (SelectedTag == null) return;

        await _activityTagFacade.SaveAsync(ActivityTagDetailModel.Empty with
        {
            TagId = SelectedTag.Id, ActivityId = Activity.Id
        });

        MessengerService.Send(new ActivityEditMessage {ActivityId = Activity.Id});
    }

    [RelayCommand]
    public async Task RemoveTagAsync(Guid tagId)
    {
        await _activityTagFacade.DeleteByActivityAndTagAsync(Activity.Id, tagId);

        MessengerService.Send(new ActivityDeleteMessage());
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        var collisions = await _activityFacade.SaveCheckDateTimeAsync(Activity with { Tags = null! });

        if (collisions.IsNullOrEmpty() == false)
        {
            var text = "Activity schedule conflicts. Please adjust to avoid collisions.\nConflicting times listed below:\n";
            foreach (var collision in collisions)
            {
                text += $"{collision.Item1} - {collision.Item2}\n";
            }
            await Application.Current.MainPage.DisplayAlert("Collisions", text, "Choose another time");
            return;
        }

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
