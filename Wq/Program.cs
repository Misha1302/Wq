namespace Wq;

using Wq.Interpreter;

public static class Program
{
    public static void Main(string[] args)
    {
        var funcDeclData = new WqFuncDeclData([
            new Instruction(InstructionType.PushConst, [123.123], []),
            new Instruction(InstructionType.PushConst, [125.125], []),
            new Instruction(InstructionType.Pow, [], [])
        ]);

        var results = new Engine().Start([funcDeclData]);
        Console.WriteLine(string.Join(", ", results));
    }
}