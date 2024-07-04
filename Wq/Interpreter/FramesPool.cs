namespace Wq.Interpreter;

public class FramesPool
{
    private readonly FastStack<WqFuncFrame> _freeFrames;

    public FramesPool(int size)
    {
        _freeFrames = new FastStack<WqFuncFrame>(size);

        for (var i = 0; i < size; i++)
            _freeFrames.Push(new WqFuncFrame());
    }

    public WqFuncFrame Rent(WqFuncDeclData declData, InterpreterData data)
    {
        var frame = _freeFrames.Pop();
        frame.Init(declData, data);
        return frame;
    }

    public void Return(WqFuncFrame frame)
    {
        _freeFrames.Push(frame);
    }
}