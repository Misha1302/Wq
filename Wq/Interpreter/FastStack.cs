namespace Wq.Interpreter;

using System.Collections;

public class FastStack<T>(int size) : IEnumerable<T>
{
    public int StackPointer;

    private readonly T[] _array = new T[size];
    public bool IsEmpty => StackPointer == 0;

    public IEnumerator<T> GetEnumerator() => _array[..StackPointer].ToList().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public void Push(T value) => _array[StackPointer++] = value;

    public T Pop() => _array[--StackPointer];

    public void Drop() => StackPointer--;

    public T Peek() => _array[StackPointer - 1];

    public void Dup() => Push(Peek());
}