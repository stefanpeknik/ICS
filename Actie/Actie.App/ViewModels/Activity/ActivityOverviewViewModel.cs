
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityOverviewViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    // User Id
    public Guid Id { get; set; }

    [ObservableProperty]
    private IEnumerable<ActivityListModel> activities = Array.Empty<ActivityListModel>();

    public ActivityOverviewViewModel( IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToAddActivityAsync()
    {
        await _navigationService.GoToAsync("/add_activity");
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await _activityFacade.GetByUserIdAsync(Id);
    }
}
