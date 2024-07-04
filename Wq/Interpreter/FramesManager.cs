namespace Wq.Interpreter;

public class FramesManager(InterpreterData data)
{
    private readonly List<WqFuncFrame> _frames = new(128);
    public WqFuncFrame CurFrame => _frames[^1];

    public void ExitFrame()
    {
        _frames.RemoveAt(_frames.Count - 1);
        CurFrame.Dispose();
    }

    public void AddFrame(WqFuncDeclData funcDeclData)
    {
        _frames.Add(new WqFuncFrame(funcDeclData, data));
    }
}