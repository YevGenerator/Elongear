using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Converters;

public class BaseIdToPathConverter : IValueConverter
{
    public string RootUrl => Http.HttpMaster.Url;

    public virtual string Part => "";

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        var url = RootUrl + Part + @"/" + value.ToString();
        return url;

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
