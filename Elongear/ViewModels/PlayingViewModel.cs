using CommunityToolkit.Mvvm.Input;
using Elongear.Http;
using Elongear.Observables;
using Elongear.Services;


namespace Elongear.ViewModels;

public partial class PlayingViewModel: BaseViewModel
{
    public Podcast Podcast { get; set; }

    [RelayCommand]
    public void LikePodcast()
    {

    }

    [RelayCommand]
    public void DislikePodcast()
    {

    }

    [RelayCommand]
    public void DownloadPodcast()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var service = ServiceHelper.GetService<FileProgressViewModel>();
        var progress = service.AddDownloadRecord(Podcast.Name);
        HttpMaster.ReceiveFileInNewSession(folder, Podcast.PodcastId, progress);

    }
}
