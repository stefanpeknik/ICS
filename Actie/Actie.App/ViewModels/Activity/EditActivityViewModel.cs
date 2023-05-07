
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.Messaging;
using Windows.System;


namespace Actie.App.ViewModels;
[QueryProperty(nameof(Id), nameof(Id))]
public partial class EditActivityViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; private set; }
    public EditActivityViewModel(IUserFacade userFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = await _userFacade.GetAsync(Id);
    }
    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
