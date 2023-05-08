using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserOverviewViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }

    [ObservableProperty]
    public UserDetailModel user;

    public UserOverviewViewModel(IUserFacade userFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = (await _userFacade.GetAsync(Id))!;
    }
    [RelayCommand]
    private async Task GoToAllProjectsAsync()
    {
        await _navigationService.GoToAsync<ProjectOverviewViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToMyProjectsAsync()
    {
        await _navigationService.GoToAsync<UserProjectOverviewViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToMyActivities()
    {
        await _navigationService.GoToAsync<ActivityOverviewViewModel>(
        new Dictionary<string, object?> { [nameof(Id)] = Id });
}


    [RelayCommand]
    private async Task GoToMyTagsAsync()
    {
        await _navigationService.GoToAsync<TagOverviewViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        await _navigationService.GoToAsync("/edit_user", new Dictionary<string, object?> { [nameof(EditUserViewModel.User)] = User });
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
