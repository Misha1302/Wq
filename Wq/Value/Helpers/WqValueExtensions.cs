namespace Wq.Value.Helpers;

using System.Runtime.CompilerServices;

public static class WqValueExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe TTo As<TFrom, TTo>(this TFrom value) where TFrom : unmanaged =>
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        *(TTo*)(&value);
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
}