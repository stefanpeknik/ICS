// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;
[QueryProperty(nameof(Id), nameof(Id))]
public partial class DetailActivityViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;

    [ObservableProperty]
    private ActivityDetailModel activity;

    public Guid Id { get; set; }

    public DetailActivityViewModel(IActivityFacade activityFacade, MessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await _activityFacade.GetAsync(Id);
    }
}
