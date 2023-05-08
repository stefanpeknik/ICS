
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
    private IEnumerable<ActivityListModel> activities = new List<ActivityListModel>();

    private DateTime _fromDate = DateTime.Today - TimeSpan.FromDays(30);
    public DateTime FromDate
    {
        get => _fromDate;
        set
        {
            if (_fromDate != value)
            {
                _fromDate = value;
                OnPropertyChanged();
                FilterOut();
            }
        }
    }

    private DateTime _toDate = DateTime.Today.AddDays(30);
    public DateTime ToDate
    {
        get => _toDate;
        set
        {
            if (_toDate != value)
            {
                _toDate = value;
                OnPropertyChanged();
                FilterOut();
            }
        }
    }

    private TimeSpan _fromTime = new(0, 0, 0);
    public TimeSpan FromTime
    {
        get => _fromTime;
        set
        {
            if (_fromTime != value)
            {
                _fromTime = value;
                OnPropertyChanged();
                FilterOut();
            }
        }
    }

    private TimeSpan _toTime = TimeSpan.FromDays(1) - TimeSpan.FromTicks(1);
    public TimeSpan ToTime
    {
        get => _toTime;
        set
        {
            if (_toTime != value)
            {
                _toTime = value;
                OnPropertyChanged();
                FilterOut();
            }
        }
    }

    

    public ActivityOverviewViewModel( IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    
    private void FilterOut()
    {
        var fromDateCombined = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hours,
            FromTime.Minutes, FromTime.Seconds);
        var toDateCombined = new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, ToTime.Hours, ToTime.Minutes, ToTime.Seconds);
        Activities = _activityFacade.GetFilteredBeforeOrAfterDateTime(Id, fromDateCombined, toDateCombined)
                         .GetAwaiter().GetResult()
                     ?? Array.Empty<ActivityListModel>();
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
