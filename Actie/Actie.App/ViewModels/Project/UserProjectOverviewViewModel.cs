
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

    // User Id
    public Guid Id { get; set; }

    [ObservableProperty]
    private IEnumerable<ProjectListModel> projects = Array.Empty<ProjectListModel>();


    public UserProjectOverviewViewModel(IProjectFacade projectFacade, IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetByUserIdAsync(Id);
    }
}
