using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Facades;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class TagOverviewViewModel : ViewModelBase, IRecipient<TagEditMessage>
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    // User Id
    public Guid Id { get; set; }

    [ObservableProperty]
    private IEnumerable<TagListModel> tags = Array.Empty<TagListModel>();

    public TagOverviewViewModel(ITagFacade tagFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToAddTagAsync()
    {
        await _navigationService.GoToAsync("/add_tag");
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Tags = await _tagFacade.GetAsync();
    }

    public async void Receive(TagEditMessage message)
    {
        await LoadDataAsync();
    }
}
