﻿using System.Collections.ObjectModel;
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
    private readonly ITagFacade _tagFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ActivityDetailModel activity;

    [ObservableProperty]
    private IList<TagListModel> tags;

    public Guid Id { get; set; }


    public DetailActivityViewModel(
        ITagFacade tagFacade,
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _tagFacade = tagFacade;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);

        Tags = (await _tagFacade.GetTagsOfActivityAsync(activity.Id)).ToList();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await _navigationService.GoToAsync("/edit_activity", new Dictionary<string, object> { [nameof(Id)] = Id });
    }
}
