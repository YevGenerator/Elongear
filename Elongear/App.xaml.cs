using Elongear.Pages;
using Elongear.Services;
using Elongear.ViewModels;

namespace Elongear;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        var page = ServiceHelper.GetService<TabMenuPage>();
        var service = ServiceHelper.GetService<NavigationService>();
        service.Navigation = page.Navigation;
        //page.BindingContext = ServiceHelper.GetService<PodcastSelectViewModel>();
        MainPage = new NavigationPage(page);
    }
}
