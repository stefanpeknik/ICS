﻿
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Actie.App.ViewModels;
[QueryProperty(nameof(User), nameof(User))]
public partial class EditUserViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public UserDetailModel user = UserDetailModel.Empty;
    public EditUserViewModel(
        IUserFacade ingredientFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = ingredientFacade;
        _navigationService = navigationService;
    }

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
