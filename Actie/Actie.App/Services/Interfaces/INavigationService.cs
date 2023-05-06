

using Actie.App.Models;
using Actie.App.ViewModels;

namespace Actie.App.Services;

public interface INavigationService
{
    IEnumerable<RouteModel> Routes { get; }

#nullable enable
    Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel;
#nullable disable

    Task GoToAsync(string route);
    bool SendBackButtonPressed();
#nullable enable
    Task GoToAsync(string route, IDictionary<string, object?> parameters);
#nullable disable

    Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel;
}
