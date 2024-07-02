namespace Wq.WqValue.Helpers;

public static class DoubleExtensions
{
    public static bool EqAprx(this double a, double b) =>
        Math.Abs(a - b) < 0.1 && Math.Abs(a - b) * 1_000_000.0 <= Math.Min(Math.Abs(a), Math.Abs(b));

    public static int Int(this double a) => (int)(a + 0.01);
}