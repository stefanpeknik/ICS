
using Actie.App.View;

namespace Actie.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<AppShell>();
    }
}
