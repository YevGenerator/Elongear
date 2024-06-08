using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class PodcastStatistic: ObservableObject
{
    public static PodcastStatistic Default =>
        new()
        {
            Likes = 0,
            Dislikes = 0,
            Views = 0,
            Downloads = 0,
        };

    [ObservableProperty]
    private int podcastId;

    [ObservableProperty]
    private int likes;

    [ObservableProperty]
    private int dislikes;

    [ObservableProperty]
    private int views;

    [ObservableProperty]
    private int downloads;

    [ObservableProperty]
    private bool isLiked;

    [ObservableProperty]
    private bool isDisliked;
   
}
