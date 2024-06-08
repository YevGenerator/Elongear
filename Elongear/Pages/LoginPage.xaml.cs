using Elongear.Services;
using Elongear.ViewModels;

namespace Elongear.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<LoginViewModel>();
    }
}