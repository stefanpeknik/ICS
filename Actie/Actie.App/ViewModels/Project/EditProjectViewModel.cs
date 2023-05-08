// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

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
public partial class EditProjectViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public ProjectDetailModel project;

    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public EditProjectViewModel(IProjectFacade projectFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _projectFacade.SaveAsync(Project with {Activities = null!, Users = null!});

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

        await _navigationService.GoToAsync<DetailProjectViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = Id, ["UserId"] = UserId });
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = await _projectFacade.GetAsync(Id);
    }
}


