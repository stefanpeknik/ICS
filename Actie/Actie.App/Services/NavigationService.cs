

#nullable enable
using Actie.App.Models;
using Actie.App.ViewModels;
using Actie.App.Views;
using Actie.App.Views.Activity;
using Actie.App.Views.Project;
using Actie.App.Views.User;
namespace Actie.App.Services;
public class NavigationService : INavigationService
{

    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(LogInView), typeof(LogInViewModel)),
        new("//users/home", typeof(UserOverviewView), typeof(UserOverviewViewModel))   
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


