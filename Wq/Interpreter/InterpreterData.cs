namespace Wq.Interpreter;

using System.Runtime.CompilerServices;
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
        InstructionExecutor = new InstructionExecutor(this);
        FramesManager = new FramesManager(this);

        FramesManager.AddFrame(functionDelcs[0]);
    }

    public Instruction[] Instructions => FramesManager.CurFrame.Instructions;
}