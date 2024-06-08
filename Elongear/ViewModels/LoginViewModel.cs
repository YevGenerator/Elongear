using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elongear.LjsonConverters;
using Elongear.Pages.SnackBars;

using Elongear.Http;

namespace Elongear.ViewModels;

public partial class LoginViewModel: BaseViewModel
{
    
    public override async void OnHttpSuccessResponse(string message)
    {
        base.OnHttpSuccessResponse("Авторизація успішна");
        await Navigate<AddPodcastViewModel>();
    }
    [ObservableProperty]
    private string loginOrEmail = "";

    [ObservableProperty]
    private string password = "";

    [RelayCommand]
    private void SignIn()
    {
        if (string.IsNullOrEmpty(LoginOrEmail))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Введіть хоч літеру нікнейма");
            return;
        }
        if (Password.Length < 8)
        {
            SnackHelper.ShowSnack<ErrorSnack>("При реєстрації мав вказуватися пароль, довший за 7 символів");
            return;
        }
        var convert = new UserLoginConverter();
        var ljson = convert.ToLjson(this);
        var sent = HttpMaster.SendSignInCommand(ljson, OnHttpSuccessResponse, OnHttpErrorResponse);
        OnSentSnack(sent);
        
    }

    [RelayCommand]
    private async Task MoveToSignUpPage()
    {
        await Navigate<RegistrationViewModel>();
    }

}
