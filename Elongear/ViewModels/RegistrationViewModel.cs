using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elongear.LjsonConverters;
using Elongear.Pages.SnackBars;
using Elongear.Http;
namespace Elongear.ViewModels;

public partial class RegistrationViewModel : BaseViewModel
{
    public override async void OnHttpSuccessResponse(string message)
    {
        SnackHelper.ShowSnack<SuccessSnack>("Реєстрація успішна");
        await Navigate<ConfirmationViewModel>();
    }
    [ObservableProperty]
    private string login = "";

    [ObservableProperty]
    private string email = "";

    [ObservableProperty]
    private string password = "";

    [ObservableProperty]
    private string passwordRepeated = "";

    [RelayCommand]
    private async Task MoveToLoginPage()
    {
        await Navigate<LoginViewModel>();
    }

    [RelayCommand]
    private async Task MoveToConfirmationPage()
    {
        await Navigate<ConfirmationViewModel>();
    }
    [RelayCommand]
    private async Task MoveToSearchPage()
    {
        await Navigate<PodcastSelectViewModel>();
    }
    [RelayCommand]
    private void SignUp()
    {
        if (string.IsNullOrEmpty(Login))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Введіть хоч літеру нікнейма");
            return;
        }
        if (Password != PasswordRepeated)
        {
            SnackHelper.ShowSnack<ErrorSnack>("Паролі не збігаються");
            return;
        }
        if(Password.Length < 8)
        {
            SnackHelper.ShowSnack<ErrorSnack>("Введіть пароль довший за 7 символів (хоча б 8)");
            return;
        }
        if(!Email.Contains('@'))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Наврядчи Ви ввели правильну пошту");
            return;
        }
        var convert = new SignUpConverter();
        var ljson = convert.ToLjson(this);
        SnackHelper.ShowSnack<WaitSnack>();
        var sent = HttpMaster.SendSignUpCommand(ljson, OnHttpSuccessResponse, OnHttpErrorResponse);
        OnSentSnack(sent);
    }
}
