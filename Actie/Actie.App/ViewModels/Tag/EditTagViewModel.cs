// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Actie.App.ViewModels;
[QueryProperty(nameof(Id), nameof(Id))]
public partial class EditTagViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    
    [ObservableProperty]
    private TagDetailModel tag;

    public Guid Id { get; set; }

    public EditTagViewModel( ITagFacade tagFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _tagFacade.SaveAsync(Tag with { Activities = null!});

        MessengerService.Send(new TagEditMessage(){ TagId = Tag.Id });

        _navigationService.SendBackButtonPressed();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Tag = await _tagFacade.GetAsync(Id);
    }
}
