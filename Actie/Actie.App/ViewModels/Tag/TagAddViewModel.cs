﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades;
using Actie.BL.Models;

namespace Actie.App.ViewModels;
public partial class TagAddViewModel : ViewModelBase
{
    public TagAddViewModel(IMessengerService messengerService) : base(messengerService)
    {
    }
}