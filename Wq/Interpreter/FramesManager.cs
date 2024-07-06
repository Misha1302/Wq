namespace Wq.Interpreter;

public class FramesManager(InterpreterData data)
{
    private readonly FastStack<WqFuncFrame> _frames = new(128);
    private readonly Pool<WqFuncFrame> _pool = MakeFramesPool();

    public WqFuncFrame CurFrame { get; private set; } = null!;
    public bool HasFrame => !_frames.IsEmpty;

    private static Pool<WqFuncFrame> MakeFramesPool()
    {
        var arrayPool = new WqFuncFrameArrayPool();
        return new Pool<WqFuncFrame>(1024, () => new WqFuncFrame(arrayPool));
    }


    public void ExitFrame()
    {
        CurFrame.Dispose();
        _pool.Return(CurFrame);
        _frames.Drop();
        SetCurFrame();
    }

    public void AddFrame(WqFuncDeclData funcDeclData)
    {
        _frames.Push(_pool.Rent());
        SetCurFrame();
        CurFrame.Init(funcDeclData, data);
    }

    private void SetCurFrame()
    {
        CurFrame = HasFrame ? _frames.Peek() : null!;
    }
}