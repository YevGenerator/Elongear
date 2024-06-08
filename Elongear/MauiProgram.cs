using CommunityToolkit.Maui;
using Elongear.Pages;
using Elongear.Services;
using Elongear.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace Elongear;

public static class MauiProgram
{
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterPages()
            .RegisterViewModels()
            .RegisterServices()
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            })
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        
        var app = builder.Build();
        ServiceHelper.Provider = app.Services;
        return app;
    }
}
