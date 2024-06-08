using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class User: ObservableObject
{
    [ObservableProperty]
    private int userId = 0;

    [ObservableProperty]
    private string name = "";

    public static User Default => new() { UserId = 0, Name = "default" };
}
