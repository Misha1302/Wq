namespace Wq.Interpreter;

using System.Runtime.CompilerServices;

public class Pool<T>
{
    private readonly FastStack<T> _freeFrames;

    public Pool(int size, Func<T> fabric)
    {
        _freeFrames = new FastStack<T>(size);

        for (var i = 0; i < size; i++)
            _freeFrames.Push(fabric());
    }

    public T Rent() => _freeFrames.Pop();

    public void Return(T frame) => _freeFrames.Push(frame);
}