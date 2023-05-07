using System.Collections.ObjectModel;
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
public partial class ProjectOverviewViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    // User Id
    public Guid? Id { get; set; } = null;

    [ObservableProperty]
    private IEnumerable<ProjectListModel> projects = Array.Empty<ProjectListModel>();

    public ProjectOverviewViewModel(IProjectFacade projectFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToAddProjectAsync()
    {
        await _navigationService.GoToAsync("/add_to_all_projects");
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = Id is null
            ? await _projectFacade.GetAsync() // gets all projects
            : await _projectFacade.GetByUserIdAsync((Guid)Id); // gets all projects of a specific user
    }
}
