namespace Wq.WqValue;

using Wq.WqValue.Helpers;

public static class WqValueOperations
{
    public static WqValue Add(WqValue a, WqValue b)
    {
        if (a.Type == b.Type)
        {
            if (a.Type == WqType.Double)
                return new WqValue(a.Get<double>() + b.Get<double>());
            if (a.Type == WqType.String)
                return new WqValue(a.Get<string>() + b.Get<string>());
        }

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__add__", a, b);

        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);
    }

    public static WqValue Sub(WqValue a, WqValue b)
    {
        if (a.Type == b.Type && a.Type == WqType.Double)
            return new WqValue(a.Get<double>() - b.Get<double>());

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__sub__", a, b);


        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);
    }

    public static WqValue Div(WqValue a, WqValue b)
    {
        if (a.Type == b.Type && a.Type == WqType.Double)
            return new WqValue(a.Get<double>() / b.Get<double>());

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__div__", a, b);


        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);
    }


    public static WqValue Pow(WqValue a, WqValue b)
    {
        if (a.Type == b.Type && a.Type == WqType.Double)
            return new WqValue(Math.Pow(a.Get<double>(), b.Get<double>()));

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__pow__", a, b);

        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);
    }


    public static WqValue Mul(WqValue a, WqValue b)
    {
        if (a.Type == b.Type && a.Type == WqType.Double)
            return new WqValue(a.Get<double>() * b.Get<double>());

        if (a.Type == WqType.String && b.Type == WqType.Double)
            return new WqValue(Repeat(a.Get<string>(), b.Get<double>().Int()));

        if (b.Type == WqType.String && a.Type == WqType.Double)
            return new WqValue(Repeat(b.Get<string>(), a.Get<double>().Int()));

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__mul__", a, b);


        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);

        static string Repeat(string text, int n)
        {
            var textAsSpan = text.AsSpan();
            var span = new Span<char>(new char[textAsSpan.Length * n]);

            for (var i = 0; i < n; i++)
                textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));

            return span.ToString();
        }
    }

    public static WqValue Rem(WqValue a, WqValue b)
    {
        if (a.Type == b.Type && a.Type == WqType.Double)
            return new WqValue(a.Get<double>() % b.Get<double>());

        if (a.Type == WqType.Class)
            return WqValueHelper.ThisCall("__rem__", a, b);

        return WqThrower.CannotOperateTypes<WqValue>(a.Type, b.Type);
    }
}