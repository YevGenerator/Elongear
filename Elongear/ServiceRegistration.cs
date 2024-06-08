using CommunityToolkit.Maui;
using Elongear.Pages;
using Elongear.Pages.Views;
using Elongear.Services;
using Elongear.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear;

public static class ServiceRegistration
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<NavigationService>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
        RegisterSingletons(builder, [
            typeof(RegistrationViewModel), 
            typeof(LoginViewModel), 
            typeof(AddPodcastViewModel), 
            typeof(PodcastSelectViewModel),
            typeof(FileProgressViewModel)
            ]);
        builder.Services.AddTransient<ConfirmationViewModel>();
        builder.Services.AddTransient<PlayingViewModel>();
        return builder;
        }
        

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        RegisterTransient(builder, [
            typeof(RegistrationPage),
            typeof(LoginPage),
            typeof(AddPodcastPage),
            typeof(ConfirmationPage),
            typeof(PodcastSearchPage),
            typeof(FileProgressPage)
            ]);
        builder.Services.AddSingleton<PlayingPage>();
        builder.Services.AddSingleton<TabMenuPage>();
        return builder;
    }

    private static MauiAppBuilder RegisterTransient(this MauiAppBuilder builder, Type[] types)
    {
        foreach (var type in types)
        {
            builder.Services.AddTransient(type);
        }
        return builder;
    }
    
    private static MauiAppBuilder RegisterSingletons(this MauiAppBuilder builder, Type[] types)
    {
        foreach (var page in types)
        {
            builder.Services.AddSingleton(page);
        }
        return builder;
    }
    
}
