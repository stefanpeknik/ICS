// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Messages;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class DetailTagViewModel : ViewModelBase, IRecipient<TagEditMessage>
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private TagDetailModel tag = TagDetailModel.Empty;

    public Guid Id { get; set; }


    public DetailTagViewModel(ITagFacade tagFacade, IMessengerService messengerService, INavigationService navigationService) : base(messengerService)
    {
        _tagFacade = tagFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToEditTagAsync()
    {
        await _navigationService.GoToAsync<EditTagViewModel>(
            new Dictionary<string, object?> { [nameof(EditTagViewModel.Id)] = Tag.Id });
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Tag = await _tagFacade.GetAsync(Id);
    }

    public async void Receive(TagEditMessage message)
    {
        await base.LoadDataAsync();
        Tag = await _tagFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Tag is not null)
        {
            await _tagFacade.DeleteAsync(Id);

            MessengerService.Send(new TagDeleteMessage());

            _navigationService.SendBackButtonPressed();

        }
    }
}
