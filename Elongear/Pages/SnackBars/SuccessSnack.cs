using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Pages.SnackBars;

class SuccessSnack: BaseSnack
{
    public SuccessSnack() :this("Операція успішна")
    {

    }
    
    public SuccessSnack(string message) : base(message)
    {
        BackColor = Colors.Lime;
        ForeColor = Colors.DarkBlue;
    }
}
