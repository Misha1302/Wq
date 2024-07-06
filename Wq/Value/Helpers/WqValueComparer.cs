namespace Wq.Value.Helpers;

public static class WqValueComparer
{
    public static bool Eq(WqValue a, WqValue b)
    {
        if (a.Type != b.Type) return false;

        return a.Type switch
        {
            WqType.Double => a.Get<double>().EqAprx(b.Get<double>()),
            WqType.String => a.Get<string>() == b.Get<string>(),
            WqType.Null => b.IsNull(),
            WqType.Class => WqValueHelper.BoolThisCall("__eq__", a, b),
            WqType.SharpObject => WqThrower.ThrowNotImplemented<bool>(),
            WqType.Bool => a.Get<bool>() == b.Get<bool>(),
            WqType.Func => a.Get<WqFunc>() == b.Get<WqFunc>(),
            _ => WqThrower.ThrowCannotCompare<bool>(a.Type, b.Type)
        };
    }

    public static bool Neq(WqValue a, WqValue b) => !Eq(a, b);

    public static bool Lt(WqValue a, WqValue b)
    {
        if (a.Type == b.Type)
        {
            if (a.Type == WqType.Double)
                return a.Get<double>() < b.Get<double>();
            if (a.Type == WqType.Class)
                return WqValueHelper.BoolThisCall("__lt__", a, b);
        }

        return WqThrower.ThrowCannotCompare<bool>(a.Type, b.Type);
    }

    public static bool Gt(WqValue a, WqValue b)
    {
        if (a.Type == b.Type)
        {
            if (a.Type == WqType.Double)
                return a.Get<double>() > b.Get<double>();
            if (a.Type == WqType.Class)
                return WqValueHelper.BoolThisCall("__gt__", a, b);
        }

        return WqThrower.ThrowCannotCompare<bool>(a.Type, b.Type);
    }

    public static bool Le(WqValue a, WqValue b)
    {
        if (a.Type == b.Type)
        {
            if (a.Type == WqType.Double)
                return a.Get<double>() <= b.Get<double>() || a.Get<double>().EqAprx(b.Get<double>());
            if (a.Type == WqType.Class)
                return WqValueHelper.BoolThisCall("__le__", a, b);
        }

        return WqThrower.ThrowCannotCompare<bool>(a.Type, b.Type);
    }

    public static bool Ge(WqValue a, WqValue b)
    {
        if (a.Type == b.Type)
        {
            if (a.Type == WqType.Double)
                return a.Get<double>() >= b.Get<double>() || a.Get<double>().EqAprx(b.Get<double>());
            if (a.Type == WqType.Class)
                return WqValueHelper.BoolThisCall("__ge__", a, b);
        }

        return WqThrower.ThrowCannotCompare<bool>(a.Type, b.Type);
    }
}