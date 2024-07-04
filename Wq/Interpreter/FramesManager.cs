namespace Wq.Interpreter;

public class FramesManager(InterpreterData data)
{
    private readonly List<WqFuncFrame> _frames = new(128);

    private readonly FramesPool _framesPool = new(1024);
    public WqFuncFrame CurFrame { get; private set; } = null!;
    public bool HasFrame => _frames.Count != 0;

    public void ExitFrame()
    {
        CurFrame.Dispose();
        _framesPool.Return(CurFrame);
        _frames.RemoveAt(_frames.Count - 1);
        SetCurFrame();
    }

    public void AddFrame(WqFuncDeclData funcDeclData)
    {
        _frames.Add(_framesPool.Rent(funcDeclData, data));
        SetCurFrame();
    }

    private void SetCurFrame()
    {
        CurFrame = HasFrame ? _frames[^1] : null!;
    }
}

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