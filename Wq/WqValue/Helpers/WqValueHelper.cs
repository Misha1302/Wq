namespace Wq.WqValue.Helpers;

using System.Runtime.CompilerServices;

public static class WqValueHelper
{
    private static ReadOnlySpan<double> RoundPower10Double => new[]
    {
        1E0, 1E1, 1E2, 1E3, 1E4, 1E5, 1E6, 1E7, 1E8,
        1E9, 1E10, 1E11, 1E12, 1E13, 1E14, 1E15
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Skip<T0, T, T1, T2, T3, T4>(out T0 value0,
        out T value1,
        out T1 value2,
        out T2 value3,
        out T3 value4,
        out T4 value5)
    {
        Unsafe.SkipInit(out value0);
        Unsafe.SkipInit(out value1);
        Unsafe.SkipInit(out value2);
        Unsafe.SkipInit(out value3);
        Unsafe.SkipInit(out value4);
        Unsafe.SkipInit(out value5);
    }

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

    public static WqValue ThisCall(string name, WqValue a, WqValue b) =>
        a.Get<WqClass>().Get(name).Get<WqFunc>().Call(a, b);
}