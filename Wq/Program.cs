namespace Wq;

using System.Diagnostics;
using Wq.Interpreter;
using Wq.Value;

public static class Program
{
    public static void Main(string[] args)
    {
        var i = WqValue.Int(0);
        const int top = 10_000_000;

        var main = new WqFuncDeclData("main",
        [
            new Instruction(InstructionType.PushConst, [0], []),
            new Instruction(InstructionType.SetLocal, [i], []),

            new Instruction(InstructionType.LoadLocal, [i], []),
            new Instruction(InstructionType.PushConst, [top], []),
            new Instruction(InstructionType.BrGe, [WqValue.Int(11 - 0)], []),

            new Instruction(InstructionType.LoadLocal, [i], []),
            new Instruction(InstructionType.Call, [WqValue.Int(1)], []),
            new Instruction(InstructionType.CallSharp, [WqValue.Nint(GetPtr()), WqValue.Int(1)], []),
            new Instruction(InstructionType.Drop, [], []),

            new Instruction(InstructionType.IncLocal, [i], []),
            new Instruction(InstructionType.Br, [WqValue.Int(2)], []),

            new Instruction(InstructionType.PushConst, [WqValue.Null], []),
            new Instruction(InstructionType.Ret, [], [])
        ], 0);

        var getStringToOutput = new WqFuncDeclData("getStringToOutput",
        [
            // return first argument
            // new Instruction(InstructionType.SetLocal, [0], []),
            // new Instruction(InstructionType.LoadLocal, [0], []),
            new Instruction(InstructionType.Ret, [], [])
        ], 1);

        for (var j = 0; j < 10; j++)
        {
            var sw = Stopwatch.StartNew();
            var results = new Engine().Start([main, getStringToOutput]);
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(string.Join(", ", results));
        }
    }

    private static nint GetPtr() =>
        typeof(Test).GetMethod(nameof(Test.Get4))!.MethodHandle.GetFunctionPointer();
}

public static class Test
{
    public static WqValue Get4(WqValue wqValue) =>
        // Console.WriteLine(wqValue);
        new WqValue(4) + wqValue;
}