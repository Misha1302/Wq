namespace Wq.Value.Helpers;

using Wq.Interpreter;

public static class WqValueFormatter
{
    public static string ToDebugString(WqValue wqValue) => $"{wqValue.ToString()} of T[{wqValue.Type}]";

    public static string ToStdString(WqValue wqValue)
    {
        return wqValue.Type switch
        {
            WqType.Double => DoubleFormatter.Format(wqValue.Get<double>()),
            WqType.String => wqValue.Get<string>(),
            WqType.SharpObject => wqValue.Get<object>().ToString() ?? "<null>",
            WqType.Null => "null",
            WqType.Invalid => WqThrower.ThrowInvalidType<string>(),
            WqType.Class => WqValueHelper.ThisCall("__str__", wqValue).Get<string>(),
            WqType.Bool => wqValue.Get<bool>().ToString(),
            WqType.Func => wqValue.Get<WqFuncDeclData>().ToString(),
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            WqType.Internal => InternalToStr(wqValue),
            _ => WqThrower.ThrowUnknownType<string>(wqValue.Type)
        };
    }

    private static string InternalToStr(WqValue wqValue)
    {
        if (wqValue.UnsafeGet<object>() is not { } obj)
            return (wqValue.UnsafeGet<long>() < 10_000 ? wqValue.UnsafeGet<long>() : 0).ToString();

        if (obj.ToString() != obj.GetType().ToString())
            return obj.ToString() ?? "null";

        return obj.GetType().Name;
    }
}