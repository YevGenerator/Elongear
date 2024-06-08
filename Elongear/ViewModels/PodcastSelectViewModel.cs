using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elongear.LjsonConverters;
using Elongear.Observables;
using Elongear.Services;

using System.Collections.ObjectModel;

using Elongear.Http;
namespace Elongear.ViewModels;

public partial class PodcastSelectViewModel: BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Podcast> podcasts = [];
    public override void OnHttpSuccessResponse(string message)
    {
        Podcasts.Clear();
        var ljsons = message.Split("\r\n");
        var converter = new PodcastConverter();
        if (ljsons[0] == "") return;
        foreach(var ljson in ljsons)
        {
            Podcasts.Add(converter.FromLjson(ljson));
        }
    }
    [RelayCommand]
    public void Search()
    {
        HttpMaster.SendSelectCommand(OnHttpSuccessResponse, OnHttpErrorResponse);
    }

    [RelayCommand]
    public async Task LoadPodcast(object obj)
    {
        if (obj is not Podcast podcast) return;
        var playingViewModel = ServiceHelper.GetService<PlayingViewModel>();
        playingViewModel.Podcast = podcast;
        await Navigate(playingViewModel);
    }
}
