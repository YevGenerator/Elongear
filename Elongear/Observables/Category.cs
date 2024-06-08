using CommunityToolkit.Mvvm.ComponentModel;
using Elongear.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class Category: ObservableObject
{
    [ObservableProperty]
    private int id = 0;

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private ObservableCollection<Podcast> podcasts = [];

    public override string ToString()
    {
        return Name;
    }

}
