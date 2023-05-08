﻿using System.Collections.ObjectModel;
using System.Diagnostics;
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
public partial class DetailActivityViewModel: ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ActivityDetailModel activity;

    public Guid Id { get; set; }


    public DetailActivityViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            await _activityFacade.DeleteAsync(Id);

            MessengerService.Send(new ActivityDeleteMessage());

            _navigationService.SendBackButtonPressed();
        }
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);
    }
}
