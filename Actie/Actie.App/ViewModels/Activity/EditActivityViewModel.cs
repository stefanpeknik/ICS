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
    public ActivityDetailModel activity;

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
        Activity = await _activityFacade.SaveAsync(Activity);

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
