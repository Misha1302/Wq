namespace Wq.WqValue.Helpers;

using System.Globalization;

public static class WqValueFormatter
{
    public static string ToDebugString(WqValue value) => $"{value.ToString()} of T[{value.Type}]";

    public static string ToStdString(WqValue value)
    {
        return value.Type switch
        {
            WqType.Double => value.Get<double>().ToString(CultureInfo.InvariantCulture),
            WqType.String => value.Get<string>(),
            WqType.SharpObject => value.Get<object>().ToString() ?? "<null>",
            WqType.Null => "null",
            _ => WqThrower.ThrowUnknownType<string>(value.Type)
        };
    }
}