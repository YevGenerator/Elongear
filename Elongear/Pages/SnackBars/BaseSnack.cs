using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Pages.SnackBars;

public class BaseSnack
{
    public ISnackbar Make() => Snackbar.Make(message: Text, duration: Duration, visualOptions: new SnackbarOptions()
    {
        BackgroundColor = BackColor,
        TextColor = ForeColor,
        CornerRadius = CornerRadius,
        Font = Font
    });

    public BaseSnack() { }
    public BaseSnack(string message) { Text = message; }

    public Color BackColor { get; set; } = Colors.AliceBlue;
    public Color ForeColor { get; set; } = Colors.DarkBlue;
    public CornerRadius CornerRadius { get; set; } = new CornerRadius(10);
    public Microsoft.Maui.Font Font { get; set; } = Microsoft.Maui.Font.Default;
    public string Text { get; set; } = "";
    public TimeSpan Duration = TimeSpan.FromSeconds(3);
}
