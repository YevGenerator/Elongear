using CommunityToolkit.Mvvm.ComponentModel;
using Elongear.Pages.SnackBars;
using Elongear.Services;


namespace Elongear.ViewModels;

public class BaseViewModel: ObservableObject
{
    protected NavigationService Navigation { get; set; }
    public BaseViewModel()
    {
        Navigation = ServiceHelper.GetService<NavigationService>();
    }
    public virtual void OnHttpErrorResponse(string message)
    {
        SnackHelper.ShowSnack<ErrorSnack>(message);
    }

    public virtual void OnHttpSuccessResponse(string message)
    {
        SnackHelper.ShowSnack<SuccessSnack>(message);
    }

    public async Task Navigate<T>() where T : BaseViewModel => await Navigation.NavigateToVmAsync<T>();
    public async Task Navigate<T>(T viewmodel) where T : BaseViewModel => await Navigation.NavigateToVmAsync(viewmodel);
    protected void OnSentSnack(bool isSent)
    {
        if (isSent)
        {
            SnackHelper.ShowSnack<WaitSnack>();
        }
        else
        {
            SnackHelper.ShowSnack<NotSentErrorSnack>();
        }
    }
}
