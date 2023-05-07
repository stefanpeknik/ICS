

using System.Reflection;
using Actie.App.Services;
using CommunityToolkit.Maui;
using Actie.BL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Actie.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureAppSettings(builder);
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services
            .AddDALServices(builder.Configuration)
            .AddAppServices()
            .AddBLServices();

        var app = builder.Build();

        app.Services.GetRequiredService<IDbMigrator>().Migrate();
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }


    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "Actie.App.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }

    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
}


