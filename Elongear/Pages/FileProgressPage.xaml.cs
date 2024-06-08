using Elongear.Services;
using Elongear.ViewModels;

namespace Elongear.Pages;

public partial class FileProgressPage : ContentPage
{
	public FileProgressPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<FileProgressViewModel>();
	}
}