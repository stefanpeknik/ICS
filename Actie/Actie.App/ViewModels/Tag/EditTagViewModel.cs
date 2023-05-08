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
[QueryProperty(nameof(Tag), nameof(Tag))]
public partial class EditTagViewModel : ViewModelBase
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public TagDetailModel tag = TagDetailModel.Empty;
    public EditTagViewModel(
        ITagFacade tagFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _tagFacade.SaveAsync(Tag);

        MessengerService.Send(new TagEditMessage { TagId = Tag.Id });

        _navigationService.SendBackButtonPressed();
    }


    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Tag is not null)
        {
            await _tagFacade.DeleteAsync(Tag.Id);

            MessengerService.Send(new TagDeleteMessage());

            _navigationService.SendBackButtonPressed();

        }
    }


    private async Task ReloadDataAsync()
    {
        Tag = await _tagFacade.GetAsync(Tag.Id)
                  ?? TagDetailModel.Empty;
    }


}
