using Elongear.Services;
using Elongear.ViewModels;

namespace Elongear.Pages;

public partial class AddPodcastPage : ContentPage
{

	public AddPodcastPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<AddPodcastViewModel>();
		PodcastImage.Source = ImageLabel.Text;
	}

    private async void PodcastImage_Clicked(object sender, EventArgs e)
    {
		var image = await FilePicker.PickAsync(new() { FileTypes=FilePickerFileType.Images});
		if (image == null) return;
		PodcastImage.Source = image.FullPath;
		ImageLabel.Text = image.FullPath;
		//result.OpenReadAsync
    }

    private async void ChoiceFileButton_Clicked(object sender, EventArgs e)
    {
		var file = await FilePicker.PickAsync(new()
		{
			FileTypes = new FilePickerFileType(
			new Dictionary<DevicePlatform, IEnumerable<string>>()
			{
				{ DevicePlatform.Android, ["audio/mpeg"] },
				{ DevicePlatform.WinUI, [".mp3", ".m4a"] }
			}
		)
		});
        if (file == null) return;
		FileName.Text = file.FullPath;
    }

    private void SuggestList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		CategoryInput.Text = SuggestList.SelectedItem.ToString();
    }
}