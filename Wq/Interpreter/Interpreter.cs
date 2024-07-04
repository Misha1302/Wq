namespace Wq.Interpreter;

using Wq.WqValue;

public class Interpreter
{
    private readonly InterpreterData _data;
    private readonly InstructionExecutor _instructionExecutor;

    public Interpreter(WqFuncDeclData[] functionDelcs)
    {
        _data = new InterpreterData(functionDelcs);
        _instructionExecutor = new InstructionExecutor(_data);
    }


    public bool Halted { get; private set; }
    public WqValue LastValue => _data.GlobalStack.Peek();

    public void Step(int stepsCount)
    {
        for (var i = 0; i < stepsCount; i++)
        {
            _instructionExecutor.ExecuteInstruction();
            if (_data.Ip < _data.Instructions.Length)
                continue;

            Halted = true;
            break;
        }
    }
}