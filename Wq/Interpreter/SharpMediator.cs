namespace Wq.Interpreter;

using Wq.WqValue;

public class SharpMediator(InterpreterData data)
{
    public WqValue Call(nint ptr, int argsCount)
    {
        return argsCount switch
        {
            0 => Call(ptr),
            1 => Call(ptr, Pop()),
            2 => Call(ptr, Pop(), Pop()),
            3 => Call(ptr, Pop(), Pop(), Pop()),
            4 => Call(ptr, Pop(), Pop(), Pop(), Pop()),
            5 => Call(ptr, Pop(), Pop(), Pop(), Pop(), Pop()),
            _ => WqThrower.ThrowNotImplemented<WqValue>()
        };

        WqValue Pop() => data.GlobalStack.Pop();
    }


    private static unsafe WqValue Call(nint ptr) =>
        ((delegate*<WqValue>)ptr)();

    private static unsafe WqValue Call(nint ptr, WqValue a1) =>
        ((delegate*<WqValue, WqValue>)ptr)(a1);

    private static unsafe WqValue Call(nint ptr, WqValue a1, WqValue a2) =>
        ((delegate*<WqValue, WqValue, WqValue>)ptr)(a1, a2);

    private static unsafe WqValue Call(nint ptr, WqValue a1, WqValue a2, WqValue a3) =>
        ((delegate*<WqValue, WqValue, WqValue, WqValue>)ptr)(a1, a2, a3);

    private static unsafe WqValue Call(nint ptr, WqValue a1, WqValue a2, WqValue a3, WqValue a4) =>
        ((delegate*<WqValue, WqValue, WqValue, WqValue, WqValue>)ptr)(a1, a2, a3, a4);

    private static unsafe WqValue Call(nint ptr, WqValue a1, WqValue a2, WqValue a3, WqValue a4, WqValue a5) =>
        ((delegate*<WqValue, WqValue, WqValue, WqValue, WqValue, WqValue>)ptr)(a1, a2, a3, a4, a5);
}