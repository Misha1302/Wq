namespace Wq.Interpreter;

public class FramesManager(InterpreterData data)
{
    private readonly List<WqFuncFrame> _frames = new(128);
    public WqFuncFrame CurFrame => _frames[^1];
    public bool HasFrame => _frames.Count != 0;

    public void ExitFrame()
    {
        CurFrame.Dispose();
        _frames.RemoveAt(_frames.Count - 1);
    }

    public void AddFrame(WqFuncDeclData funcDeclData)
    {
        _frames.Add(new WqFuncFrame(funcDeclData, data));
    }
}