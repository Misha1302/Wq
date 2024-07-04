namespace Wq.Value.Helpers;

using System.Globalization;

public static class DoubleFormatter
{
    public static string Format(double value)
    {
        if (value is > 0.0001 and < 1_000_000 or < -0.0001 and > -1_000_000)
            return value.ToString("0.0#####", CultureInfo.InvariantCulture);
        return value.ToString("e6", CultureInfo.InvariantCulture);
    }
}