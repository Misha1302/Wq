namespace Wq.Interpreter;

using Wq.Value;

public class Interpreter
{
    private readonly InterpreterData _data;
    private readonly InstructionExecutor _instructionExecutor;
    public LaunchMode LaunchMode;

    public Interpreter(WqFuncDeclData[] functionDelcs)
    {
        _data = new InterpreterData(functionDelcs);
        _instructionExecutor = new InstructionExecutor(_data);
    }


    public bool Halted => _data.Halted;
    public WqValue LastWqValue => _data.GlobalStack.Peek();

    public void Step(int stepsCount)
    {
        for (var i = 0; i < stepsCount; i++)
        {
            var instruction = _data.Instructions[_data.FramesManager.CurFrame.Ip];
            _instructionExecutor.ExecuteInstruction();

            if (LaunchMode == LaunchMode.Debug)
                PrintState(instruction);

            if (!_data.Halted && _data.FramesManager.CurFrame.Ip < _data.Instructions.Length)
                continue;

            _data.Halted = true;
            break;
        }
    }

    private void PrintState(Instruction i)
    {
        Console.WriteLine(
            !_data.Halted
                ? $"{i} -> {_data.FramesManager.CurFrame.Ip} -> [{string.Join(", ", _data.GlobalStack)}]"
                : "Halted"
        );
    }
}