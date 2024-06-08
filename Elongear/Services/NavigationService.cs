using Elongear.Pages;
using Elongear.Pages.Views;
using Elongear.ViewModels;

namespace Elongear.Services;

public class NavigationService
{
    private Dictionary<Type, Func<BaseViewModel, Task>> ViewModelPageMap { get; set; }
    public NavigationService()
    {
        ViewModelPageMap = new()
        {
            {typeof(RegistrationViewModel), NavigateAsync<RegistrationPage> },
            {typeof(LoginViewModel), NavigateAsync<LoginPage> },
            {typeof(ConfirmationViewModel), NavigateAsync<ConfirmationPage> },
            {typeof(AddPodcastViewModel), NavigateAsync<AddPodcastPage>},
            {typeof(PodcastSelectViewModel),NavigateAsync<PodcastSearchPage>},
            {typeof(PlayingViewModel),NavigateAsync<PlayingPage> }
        };
    }
    public Page RootPage => Navigation.NavigationStack[0];
    public Page CurrentPage => Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
    public INavigation Navigation { get; set; }

    public async Task NavigateAsync(Page page)
    {
        await MainThread.InvokeOnMainThreadAsync(async ()=> { await Navigation.PushAsync(page); });
    }

 
    public async Task NavigateToMainAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(Navigation.PopToRootAsync);
    }
    public async Task NavigateAsync<TPage>() where TPage: Page
    {
        var page = ServiceHelper.GetService<TPage>();
        if(Equals(page, RootPage))
        {
            await NavigateToMainAsync();
            return;
        }
        await NavigateAsync(page);
    }

    public async Task NavigateAsync<TPage>(BaseViewModel viewModel) 
        where TPage: Page
    {
        var page = ServiceHelper.GetService<TPage>();
        page.BindingContext = viewModel;
        if (Equals(page, RootPage))
        {
            await NavigateToMainAsync();
            return;
        }
        await NavigateAsync(page);
    }
    public async Task GoBackAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(async () => await Navigation.PopAsync());
    }

    public async Task NavigateToVmAsync<TModel>(TModel model) where TModel: BaseViewModel
    {
        await ViewModelPageMap[typeof(TModel)](model);
    }

    public async Task NavigateToVmAsync<TModel>() where TModel : BaseViewModel
    {
        var model = ServiceHelper.GetService<TModel>();
        await NavigateToVmAsync(model);
    }

}
