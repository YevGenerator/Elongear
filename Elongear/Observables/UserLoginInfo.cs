using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class UserLoginInfo: ObservableObject
{

    [ObservableProperty]
    private string userName = "";

    [ObservableProperty]
    private string email = "";

    [ObservableProperty]
    private string password = "";
}
