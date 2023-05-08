using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Actie.App.ViewModels;

public partial class LogInViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private IEnumerable<UserListModel> users = Array.Empty<UserListModel>();

    public LogInViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }


    [RelayCommand]
    private async Task GoToAddUserAsync()
    {
        await _navigationService.GoToAsync("/add_user");
    }

    [RelayCommand]
    private async Task LogInAsync(Guid id)
    {
        await _navigationService.GoToAsync<UserOverviewViewModel>(
            new Dictionary<string, object?> { [nameof(UserOverviewViewModel.Id)] = id });
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
