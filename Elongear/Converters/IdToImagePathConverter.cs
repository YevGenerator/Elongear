using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Converters;

public class IdToImagePathConverter : BaseIdToPathConverter
{
    public override string Part => "images";
}
