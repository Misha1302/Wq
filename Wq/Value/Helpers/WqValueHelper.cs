namespace Wq.Value.Helpers;

using System.Runtime.CompilerServices;

public static class WqValueHelper
{
    private static ReadOnlySpan<double> RoundPower10Double => new[]
    {
        1E0, 1E1, 1E2, 1E3, 1E4, 1E5, 1E6, 1E7, 1E8,
        1E9, 1E10, 1E11, 1E12, 1E13, 1E14, 1E15
    };

    public static long Hash(double d, object sharpObject, WqType type)
    {
        // dangerous 'rounding'!
        var a = PowAndRound(d, 5); // 2.0001 -> 200010
        var b = Unsafe.As<object, long>(ref sharpObject);
        var c = (long)type;

        return a ^ b ^ c;
    }

    private static long PowAndRound(double value, int digits) => (value * RoundPower10Double[digits]).Int();


    public static bool BoolThisCall(string name, WqValue a, WqValue b) =>
        ThisCall(name, a, b).Get<bool>();

    public static WqValue ThisCall(string name, WqValue a) =>
        a.Get<WqClass>().Get(name).Get<WqFunc>().Call(a);

    public static WqValue ThisCall(string name, WqValue a, WqValue b) =>
        a.Get<WqClass>().Get(name).Get<WqFunc>().Call(a, b);
}