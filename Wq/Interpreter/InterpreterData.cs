namespace Wq.Interpreter;

public class InterpreterData
{
    public bool Halted;
    public readonly InterpreterStack GlobalStack = new(1024);
    public readonly FramesManager FramesManager;
    public readonly WqFuncDeclData[] FunctionDelcs;
    public readonly SharpMediator SharpMediator;


    public InterpreterData(WqFuncDeclData[] functionDelcs)
    {
        FunctionDelcs = functionDelcs;
        SharpMediator = new SharpMediator(this);
        FramesManager = new FramesManager(this);

        FramesManager.AddFrame(functionDelcs[0]);
    }

    public Instruction[] Instructions => FramesManager.CurFrame.Instructions;
}