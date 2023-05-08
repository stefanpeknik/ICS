// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Services;
using Actie.BL.Facades.Interfaces;
using Actie.BL.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Actie.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class EditActivityViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [ObservableProperty]
    public ActivityDetailModel activity;

    [ObservableProperty]
    public IEnumerable<ProjectListModel> myProjects;

    [ObservableProperty]
    public ProjectListModel selectedProject;

    public EditActivityViewModel(IProjectFacade projectFacade, IActivityFacade activityFacade, IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);
        MyProjects = await _projectFacade.GetByUserIdAsync(UserId);
    }
}
