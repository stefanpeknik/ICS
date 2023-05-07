﻿

#nullable enable
using Actie.App.Models;
using Actie.App.ViewModels;
using Actie.App.Views;
using Actie.App.Views.Activity;
using Actie.App.Views.Project;
using Actie.App.Views.Tag;
using Actie.App.Views.User;
namespace Actie.App.Services;
public class NavigationService : INavigationService
{

    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(LogInView), typeof(LogInViewModel)),
        new("//users/add", typeof(AddUserView), typeof(AddUserViewModel)),

        new("//users/home", typeof(UserOverviewView), typeof(UserOverviewViewModel)),
        new("//users/home/all_projects", typeof(ProjectOverviewView), typeof(ProjectOverviewViewModel)),
        new("//users/home/my_projects", typeof(UserProjectOverviewView), typeof(UserProjectOverviewViewModel)),
        new("//users/home/activities", typeof(ActivityOverviewView), typeof(ActivityOverviewViewModel)),
        new("//users/home/tags", typeof(TagOverviewView), typeof(TagOverviewViewModel)),
        

        new("//users/home/all_projects/add", typeof(AddProjectView), typeof(AddProjectViewModel)),
        new("//users/home/all_projects/detail", typeof(AddProjectView), typeof(AddProjectViewModel)), //TODO detail
        new("//users/home/all_projects/detail/edit", typeof(AddProjectView), typeof(AddProjectViewModel)), //TODO edit

        new("//users/home/my_projects/add", typeof(AddProjectView), typeof(AddProjectViewModel)),
        new("//users/home/my_projects/detail", typeof(AddProjectView), typeof(AddProjectViewModel)), //TODO detail
        new("//users/home/my_projects/detail/edit", typeof(AddProjectView), typeof(AddProjectViewModel)), //TODO edit

        new("//users/home/activities/add", typeof(AddUserView), typeof(AddUserViewModel)), //TODO add
        new("//users/home/activities/detail", typeof(AddUserView), typeof(AddUserViewModel)), //TODO detail
        new("//users/home/activities/detail/edit", typeof(AddUserView), typeof(AddUserViewModel)), //TODO edit

        new("//users/home/tags/add", typeof(AddUserView), typeof(AddUserViewModel)), //TODO add
        new("//users/home/tags/detail", typeof(AddUserView), typeof(AddUserViewModel)), //TODO detail
        new("//users/home/tags/detail/edit", typeof(AddUserView), typeof(AddUserViewModel)), //TODO edit
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}


