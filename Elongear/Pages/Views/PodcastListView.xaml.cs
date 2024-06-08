using Elongear.Observables;
using System.Collections;

namespace Elongear.Pages.Views;

public partial class PodcastListView : ContentView
{
    public static readonly BindableProperty PodcastListProperty =
        BindableProperty.Create(nameof(PodcastList), typeof(string), typeof(ConfirmationDigits), string.Empty);

    public IEnumerable<Podcast> PodcastList
    {
        get => (IEnumerable<Podcast>)GetValue(PodcastListProperty);
        set => SetValue(PodcastListProperty, value);
    }

    public PodcastListView()
	{
		InitializeComponent();
	}
}