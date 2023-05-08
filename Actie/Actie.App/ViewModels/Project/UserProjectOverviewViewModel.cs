
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserProjectOverviewViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;


    // User Id
    public Guid Id { get; set; }

    [ObservableProperty]
    private IEnumerable<ProjectListModel> projects = Array.Empty<ProjectListModel>();


    public UserProjectOverviewViewModel(IProjectFacade projectFacade, INavigationService navigationService, IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;

    }

    [RelayCommand]
    private async Task GoToAddProjectAsync()
    {
        await _navigationService.GoToAsync<AddProjectViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToProjectDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<DetailProjectViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = id });
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetByUserIdAsync(Id);
    }
}
