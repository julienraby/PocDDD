using System.Globalization;
using System.Text.RegularExpressions;

namespace PocDDD.Api.Common;

public static class Utility
{
    public static string? FormatNumber<T>(this T number, string culture)
    {
        if (number == null)
            return null;

        const string format = "C";
        var numberFormat = new CultureInfo(culture, false).NumberFormat;
        return float.TryParse(number.ToString(), out float value) ? value.ToString(format, numberFormat) : number.ToString();
    }
}
