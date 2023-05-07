using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;

namespace Actie.App.ViewModels;
public partial class TagOverviewViewModel : ViewModelBase
{
    public TagOverviewViewModel(IMessengerService messengerService) : base(messengerService)
    {
    }
}
