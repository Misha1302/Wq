namespace Wq.Interpreter;

using Wq.Value;

public class InterpreterData
{
    public bool Halted;
    public readonly FastStack<WqValue> GlobalStack = new(1024);
    public readonly FramesManager FramesManager;
    public readonly WqFuncDeclData[] FunctionDelcs;
    public readonly SharpMediator SharpMediator;
    public readonly InstructionExecutor InstructionExecutor;


    public InterpreterData(WqFuncDeclData[] functionDelcs)
    {
        FunctionDelcs = functionDelcs;
        SharpMediator = new SharpMediator(this);
        FramesManager = new FramesManager(this);
        InstructionExecutor = new InstructionExecutor(this);

        FramesManager.AddFrame(functionDelcs[0]);
    }

    public Instruction[] Instructions => FramesManager.CurFrame.Instructions;
}