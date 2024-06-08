using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Pages.SnackBars;

public class NotSentErrorSnack: ErrorSnack
{
    public NotSentErrorSnack() : base("Повідомлення не надіслано. Можливо, немає зв'язку із сервером") { }
}
