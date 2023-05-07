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
public partial class TagOverviewViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;

    // User Id
    public Guid Id { get; set; }

    [ObservableProperty]
    private IEnumerable<TagListModel> tags = Array.Empty<TagListModel>();

    public TagOverviewViewModel(ITagFacade tagFacade, IMessengerService messengerService) : base(messengerService)
    {
        _tagFacade = tagFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Tags = await _tagFacade.GetByUserIdAsync(Id);
    }
}
