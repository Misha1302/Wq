namespace Wq.Interpreter;

using System.Buffers;
using System.Diagnostics;
using Wq.Value;

public class WqFuncFrame : IDisposable
{
    public int Ip;

    private int _startStackPointer;
    private WqFuncDeclData _declData = null!;
    private InterpreterData _data = null!;

    public WqValue[] Locals { get; private set; } = null!;

    public Instruction[] Instructions => _declData.Instructions;

    public void Dispose()
    {
        Debug.Assert(_startStackPointer + 1 == _data.GlobalStack.StackPointer);
        ArrayPool<WqValue>.Shared.Return(Locals);
        GC.SuppressFinalize(this);
    }

    public void Init(WqFuncDeclData declData, InterpreterData data)
    {
        _declData = declData;
        _data = data;
        Locals = ArrayPool<WqValue>.Shared.Rent(declData.LocalsCount);
        _startStackPointer = data.GlobalStack.StackPointer;
        Ip = 0;
    }
}