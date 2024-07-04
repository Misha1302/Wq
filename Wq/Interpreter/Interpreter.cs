namespace Wq.Interpreter;

using Wq.Value;

public class Interpreter(WqFuncDeclData[] functionDelcs)
{
    private readonly InterpreterData _data = new(functionDelcs);
    public LaunchMode LaunchMode;


    public bool Halted => _data.Halted;
    public WqValue LastWqValue => _data.GlobalStack.Peek();

    public void Step(int stepsCount)
    {
        for (var i = 0; i < stepsCount; i++)
        {
            var instruction = LaunchMode == LaunchMode.Debug
                ? _data.Instructions[_data.FramesManager.CurFrame.Ip]
                : null!;

            _data.InstructionExecutor.ExecuteInstruction();

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
                : $"Halted -> [{string.Join(", ", _data.GlobalStack)}]"
        );
    }
}