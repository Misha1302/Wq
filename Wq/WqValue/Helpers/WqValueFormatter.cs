namespace Wq.WqValue.Helpers;

public static class WqValueFormatter
{
    public static string ToDebugString(WqValue value) => $"{value.ToString()} of T[{value.Type}]";

    public static string ToStdString(WqValue value)
    {
        return value.Type switch
        {
            WqType.Double => DoubleFormatter.Format(value.Get<double>()),
            WqType.String => value.Get<string>(),
            WqType.SharpObject => value.Get<object>().ToString() ?? "<null>",
            WqType.Null => "null",
            _ => WqThrower.ThrowUnknownType<string>(value.Type)
        };
    }
}