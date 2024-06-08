using Elongear.Pages.SnackBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Pages.SnackBars;

public static class SnackHelper
{
    public static async void ShowSnack<T>() where T: BaseSnack, new()
    {
        var snack = new T().Make();
        await snack.Show();
    }
    public static async void ShowSnack<T>(string message) where T : BaseSnack, new()
    {
        var snack = new T() { Text = message };
        await snack.Make().Show();
    }
}
