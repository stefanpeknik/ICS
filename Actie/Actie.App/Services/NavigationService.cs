

using Actie.App.Models;
using Actie.App.ViewModels;
using Actie.App.ViewModels.Project;
using Actie.App.ViewModels.User;
using Actie.App.Views.Activity;
using Actie.App.Views.Project;
using Actie.App.Views.User;
namespace Actie.App.Services;
public class NavigationService : INavigationService
{

    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//intro", typeof(IntroView), typeof(IntroViewModel)),
        new("//user/add", typeof(AddUserView), typeof(AddUserViewModel)),

        new("//project/add", typeof(AddProjectView), typeof(AddProjectViewModel)),
        new("//overview/project", typeof(ProjectOverviewView), typeof(ProjectOverviewViewModel)),
        new("//overview/user/project", typeof(UserProjectOverviewView), typeof(UserProjectOverviewViewModel)),

        new("//overview/activity", typeof(ActivityOverviewView), typeof(ActivityOverviewViewModel)),
        
    };

    public Task GoToAsync<TViewModel>(IDictionary<string, object> parameters) where TViewModel : IViewModel => throw new NotImplementedException();
    public Task GoToAsync(string route) => throw new NotImplementedException();
    public Task GoToAsync(string route, IDictionary<string, object> parameters) => throw new NotImplementedException();
    public Task GoToAsync<TViewModel>() where TViewModel : IViewModel => throw new NotImplementedException();
    public bool SendBackButtonPressed() => throw new NotImplementedException();
}


