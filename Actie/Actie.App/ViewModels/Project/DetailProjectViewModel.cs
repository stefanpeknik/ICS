
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
[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class DetailProjectViewModel : ViewModelBase, IRecipient<ProjectEditMessage>
{
    private readonly IUserProjectFacade _userProjectFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SeeJoin))]
    [NotifyPropertyChangedFor(nameof(SeeLeave))]
    private ProjectDetailModel project;

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    public DetailProjectViewModel(
        IUserProjectFacade userProjectFacade,
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _userProjectFacade = userProjectFacade;
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            await _projectFacade.DeleteAsync(Project.Id);

            MessengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();

        }
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(Id);
    }

    
    public bool SeeJoin =>  Project is null ? false : !Project.Users.Any(up => up.UserId == UserId);
    public bool SeeLeave => Project is null ? false : Project.Users.Any(up => up.UserId == UserId);

    [RelayCommand]
    private async Task JoinProjectAsync()
    {
        await _userProjectFacade.SaveAsync(UserProjectDetailModel.Empty with {ProjectId = Project.Id, UserId = UserId});

        MessengerService.Send(new ProjectEditMessage {ProjectId = Project.Id} );
    }

    [RelayCommand]
    private async Task LeaveProjectAsync()
    {
        var upToDelete = Project.Users.First(up => up.UserId == UserId);
            await _userProjectFacade.DeleteAsync(upToDelete.Id);

        MessengerService.Send(new ProjectEditMessage{ProjectId = Project.Id});
    }

    [RelayCommand]
    private async Task GoToActivityDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<DetailActivityViewModel>(
            new Dictionary<string, object?> { [nameof(Id)] = id });
    }

    [RelayCommand]
    private async Task GoToEditProjectAsync()
    {
        await _navigationService.GoToAsync<EditProjectViewModel>(
            new Dictionary<string, object?> { [nameof(EditProjectViewModel.Id)] = Project.Id , ["UserId"] = UserId });
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
        OnPropertyChanged(nameof(SeeJoin));
    }
}


