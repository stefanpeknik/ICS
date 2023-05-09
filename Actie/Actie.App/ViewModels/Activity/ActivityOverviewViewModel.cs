
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityOverviewViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    public class DateRangePicker
    {
        public string Range { get; set; }
        public DateTime ToDate
        {
            get => DateTime.Today;
        }

        public DateTime FromDate { get; set; }
    }

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
                if (FromDate > ToDate)
                    _fromDate = ToDate - TimeSpan.FromHours(1);
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
                if (ToDate < FromDate)
                    _toDate = FromDate + TimeSpan.FromHours(1);
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

    [ObservableProperty]
    public static IList<DateRangePicker> pickerItems;

    private DateRangePicker _selectedPicker;
    public DateRangePicker SelectedPicker
    {
        get => _selectedPicker;
        set
        {
            if (_selectedPicker != value)
            {
                _selectedPicker = value;
                FromDate = value.FromDate;
                ToDate = value.ToDate;
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

        PickerItems = new List<DateRangePicker>
            {
                new() {Range = "Last week", FromDate = DateTime.Today - TimeSpan.FromDays(7)},
                new() {Range = "Last month", FromDate = DateTime.Today - TimeSpan.FromDays(30)},
                new() {Range = "Previous month", FromDate = DateTime.Today - TimeSpan.FromDays(60)},
                new() {Range = "Last year", FromDate = DateTime.Today - TimeSpan.FromDays(365)}
            };
        SelectedPicker = PickerItems[3];
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
        await _navigationService.GoToAsync("/add_activity", new Dictionary<string, object?> { [nameof(Id)] = Id});
    }

    [RelayCommand]
    private async Task GoToActivityDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<DetailActivityViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = id, ["UserId"] = Id });
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await _activityFacade.GetByUserIdAsync(Id);
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
