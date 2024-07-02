namespace Wq.WqValue.Helpers;

using System.Runtime.CompilerServices;

public record WqFunc(nint Ptr, int ArgsCount)
{
    public unsafe WqFunc(delegate*<WqValue, WqValue, WqValue> ptr) : this((nint)ptr, 2)
    {
    }

    public unsafe WqFunc(delegate*<WqValue, WqValue> ptr) : this((nint)ptr, 1)
    {
    }

    public unsafe WqFunc(delegate*<WqValue> ptr) : this((nint)ptr, 0)
    {
    }

    public unsafe WqValue Call()
    {
        EnsureArgsCountValid(0);
        return ((delegate*<WqValue>)Ptr)();
    }

    public unsafe WqValue Call(WqValue a1)
    {
        EnsureArgsCountValid(1);
        return ((delegate*<WqValue, WqValue>)Ptr)(a1);
    }

    public unsafe WqValue Call(WqValue a1, WqValue a2)
    {
        EnsureArgsCountValid(2);
        return ((delegate*<WqValue, WqValue, WqValue>)Ptr)(a1, a2);
    }

    public unsafe WqValue Call(WqValue a1, WqValue a2, WqValue a3)
    {
        EnsureArgsCountValid(3);
        return ((delegate*<WqValue, WqValue, WqValue, WqValue>)Ptr)(a1, a2, a3);
    }

    public unsafe WqValue Call(WqValue a1, WqValue a2, WqValue a3, WqValue a4)
    {
        EnsureArgsCountValid(4);
        return ((delegate*<WqValue, WqValue, WqValue, WqValue, WqValue>)Ptr)(a1, a2, a3, a4);
    }


    [MethodImpl(MethodImplOptions.NoInlining)]
    private void EnsureArgsCountValid(int currentCount)
    {
        if (ArgsCount != currentCount)
            WqThrower.ThrowInvalidArgsCount<WqValue>(currentCount, ArgsCount);
    }
}