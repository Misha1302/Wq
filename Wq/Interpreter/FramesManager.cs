namespace Wq.Interpreter;

public class FramesManager(InterpreterData data)
{
    private readonly FastStack<WqFuncFrame> _frames = new(128);

    private readonly FramesPool _framesPool = new(1024);
    public WqFuncFrame CurFrame { get; private set; } = null!;
    public bool HasFrame => !_frames.IsEmpty;

    public void ExitFrame()
    {
        CurFrame.Dispose();
        _framesPool.Return(CurFrame);
        _frames.Drop();
        SetCurFrame();
    }

    public void AddFrame(WqFuncDeclData funcDeclData)
    {
        _frames.Push(_framesPool.Rent(funcDeclData, data));
        SetCurFrame();
    }

    private void SetCurFrame()
    {
        CurFrame = HasFrame ? _frames.Peek() : null!;
    }
}