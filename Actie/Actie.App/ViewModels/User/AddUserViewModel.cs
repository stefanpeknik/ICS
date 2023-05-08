// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using ABI.Windows.System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades;
using Actie.BL.Models;
using Actie.BL.Facades.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;


public partial class AddUserViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid Id { get; set; }

    [ObservableProperty] public UserDetailModel user = UserDetailModel.Empty;


    public AddUserViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }



    public string Name
    {
        get => User.Name;
        set
        {
            if (User.Name != value)
            {
                User.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public string Surname
    {
        get => User.Surname;
        set
        {
            if (User.Surname != value)
            {
                User.Surname = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }
    public bool CanSave => User.Name != string.Empty && User.Surname != string.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _userFacade.SaveAsync(User);

        MessengerService.Send(new UserEditMessage { UserId = User.Id });

        _navigationService.SendBackButtonPressed();
    }

    
    public async void Receive(UserEditMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        User = await _userFacade.GetAsync(User.Id)
                 ?? UserDetailModel.Empty;
    }
}
