namespace Wq.Interpreter;

using Wq.WqValue;

public class InterpreterStack(int size)
{
    public int StackPointer;

    private readonly WqValue[] _array = new WqValue[size];

    public void Push(WqValue value) => _array[StackPointer++] = value;

    public WqValue Pop() => _array[--StackPointer];

    public void Drop() => StackPointer--;

    public WqValue Peek() => _array[StackPointer - 1];

    public void Dup() => Push(Peek());
}