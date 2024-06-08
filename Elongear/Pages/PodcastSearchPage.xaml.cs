using Elongear.Services;
using Elongear.ViewModels;

namespace Elongear.Pages;

public partial class PodcastSearchPage : ContentPage
{
	public PodcastSearchPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<PodcastSelectViewModel>();
    }
}