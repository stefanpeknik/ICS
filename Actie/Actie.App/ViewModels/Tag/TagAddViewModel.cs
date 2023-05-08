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
public partial class TagAddViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }

    [ObservableProperty]
    public TagDetailModel tag = TagDetailModel.Empty;

    public TagAddViewModel(ITagFacade tagFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
    }
    public string Name
    {
        get => Tag.Name;
        set
        {
            if (Tag.Name != value)
            {
                Tag.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }
   
    public bool CanSave => Tag.Name != string.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _tagFacade.SaveAsync(Tag);

        MessengerService.Send(new TagEditMessage { TagId = Tag.Id });

        _navigationService.SendBackButtonPressed();
    }
}
