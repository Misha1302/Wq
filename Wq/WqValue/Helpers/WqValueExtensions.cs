namespace Wq.WqValue.Helpers;

public static class WqValueExtensions
{
    public static TTo As<TFrom, TTo>(this TFrom value) => (TTo)(object)value!;
}