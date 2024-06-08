using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class Podcast: ObservableObject
{
    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string podcastId = "0";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private DateTime uploadTime = DateTime.Now;

    [ObservableProperty]
    private Observables.User user = Observables.User.Default;

    [ObservableProperty]
    private Observables.PodcastStatistic statistic;

    [ObservableProperty]
    private ObservableCollection<string> tags = [];
       

}
