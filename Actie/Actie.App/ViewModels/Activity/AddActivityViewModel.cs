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
using Microsoft.IdentityModel.Tokens;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class AddActivityViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }

    [ObservableProperty]
    public IList<ProjectListModel> myProjects;

    [ObservableProperty]
    public ProjectListModel selectedProject;

    [ObservableProperty]
    public ActivityDetailModel activityModel = ActivityDetailModel.Empty with {Start = DateTime.Now - TimeSpan.FromHours(1), End = DateTime.Now};

    public string Name
    {
        get => ActivityModel.Name;
        set
        {
            if (ActivityModel.Name != value)
            {
                ActivityModel.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public string Type
    {
        get => ActivityModel.Type;
        set
        {
            if (ActivityModel.Type != value)
            {
                ActivityModel.Type = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public DateTime StartDate
    {
        get => ActivityModel.Start;
        set
        {
            if (ActivityModel.Start != value)
            {
                TimeSpan time = ActivityModel.Start.TimeOfDay;
                ActivityModel.Start = new DateTime(value.Year, value.Month, value.Day);
                ActivityModel.Start = ActivityModel.Start.Add(time);
                if (ActivityModel.Start > ActivityModel.End)
                    ActivityModel.Start = ActivityModel.End - TimeSpan.FromHours(1);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public DateTime EndDate
    {
        get => ActivityModel.End;
        set
        {
            if (ActivityModel.End != value)
            {
                TimeSpan time = ActivityModel.End.TimeOfDay;
                ActivityModel.End = new DateTime(value.Year, value.Month, value.Day);
                ActivityModel.End = ActivityModel.End.Add(time);
                if (ActivityModel.End < ActivityModel.Start)
                    ActivityModel.End = ActivityModel.Start + TimeSpan.FromHours(1);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public TimeSpan StartTime
    {
        get => ActivityModel.Start.TimeOfDay;
        set
        {
            if (ActivityModel.Start.TimeOfDay != value)
            {
                ActivityModel.Start = new DateTime(ActivityModel.Start.Year, ActivityModel.Start.Month, ActivityModel.Start.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public TimeSpan EndTime
    {
        get => ActivityModel.End.TimeOfDay;
        set
        {
            if (ActivityModel.End.TimeOfDay != value)
            {
                ActivityModel.End = new DateTime(ActivityModel.End.Year, ActivityModel.End.Month, ActivityModel.End.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public bool CanSave => ActivityModel.Name != string.Empty && ActivityModel.Type != string.Empty && !MyProjects.IsNullOrEmpty() &&
                           (ActivityModel.Start < ActivityModel.End);

    public AddActivityViewModel(IProjectFacade projectFacade, IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        MyProjects = (await _projectFacade.GetByUserIdAsync(Id)).ToList();

        SelectedProject = MyProjects.FirstOrDefault();
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        var collisions = await _activityFacade.SaveCheckDateTimeAsync(ActivityModel with {UserId = Id, ProjectId = SelectedProject.Id});

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

        MessengerService.Send(new ActivityEditMessage{ ActivityId = ActivityModel.Id });

        _navigationService.SendBackButtonPressed();
    }
}
