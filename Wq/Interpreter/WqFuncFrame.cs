namespace Wq.Interpreter;

using System.Buffers;
using System.Diagnostics;
using Wq.WqValue;

public class WqFuncFrame(WqFuncDeclData declData, InterpreterData data) : IDisposable
{
    public readonly WqValue[] Locals = ArrayPool<WqValue>.Shared.Rent(declData.LocalsCount);

    private readonly int _startStackPointer = data.GlobalStack.StackPointer;
    public Instruction[] Instructions => declData.Instructions;

    public void Dispose()
    {
        Debug.Assert(_startStackPointer + 1 == data.GlobalStack.StackPointer);
        ArrayPool<WqValue>.Shared.Return(Locals);
        GC.SuppressFinalize(this);
    }
}