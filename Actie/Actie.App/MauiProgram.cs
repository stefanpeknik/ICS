

using CommunityToolkit.Maui;
using Actie.BL;
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

        // Continue initializing your .NET MAUI App here

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services
            //.AddDALServices(builder.Configuration) // TODO
            .AddAppServices()
            .AddBLServices();

        return builder.Build();
    }
}
