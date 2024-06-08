using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Pages.SnackBars;

public class ErrorSnack : BaseSnack
{
    public ErrorSnack() : this("Непередбачувана помилка")
    {

    }
    public ErrorSnack(string message) : base(message)
    {
        BackColor = Colors.Orange;
        ForeColor = Colors.Black;
        Duration = TimeSpan.FromSeconds(5);
    }

}
