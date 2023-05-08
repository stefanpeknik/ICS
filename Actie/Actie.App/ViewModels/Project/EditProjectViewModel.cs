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
[QueryProperty(nameof(Project), nameof(Project))]
public partial class EditProjectViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public ProjectDetailModel project = ProjectDetailModel.Empty;
    public EditProjectViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _projectFacade.SaveAsync(Project);

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

        _navigationService.SendBackButtonPressed();
    }


    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            await _projectFacade.DeleteAsync(Project.Id);

            MessengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();

        }
    }


    private async Task ReloadDataAsync()
    {
        Project = await _projectFacade.GetAsync(Project.Id)
               ?? ProjectDetailModel.Empty;
    }

    
}
