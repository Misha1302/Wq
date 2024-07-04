namespace Wq.Interpreter;

using System.Collections;
using Wq.Value;

public class InterpreterStack(int size) : IEnumerable<WqValue>
{
    public int StackPointer;

    private readonly WqValue[] _array = new WqValue[size];

    public IEnumerator<WqValue> GetEnumerator() => _array[..StackPointer].ToList().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public void Push(WqValue wqValue) => _array[StackPointer++] = wqValue;

    public WqValue Pop() => _array[--StackPointer];

    public void Drop() => StackPointer--;

    public WqValue Peek() => _array[StackPointer - 1];

    public void Dup() => Push(Peek());
}