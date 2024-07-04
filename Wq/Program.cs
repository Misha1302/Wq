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
        // const int top = 100_000;

        var main = new WqFuncDeclData("main",
        [
            new Instruction(InstructionType.PushConst, [0], []),
            new Instruction(InstructionType.SetLocal, [i], []),

            new Instruction(InstructionType.LoadLocal, [i], []),
            new Instruction(InstructionType.PushConst, [top], []),
            new Instruction(InstructionType.BrGe, [WqValue.Int(13 - 1)], []),

            new Instruction(InstructionType.Call, [WqValue.Int(1)], []),
            // new Instruction(InstructionType.CallSharp, [WqValue.Nint(GetPtr()), WqValue.Int(1)], []),
            new Instruction(InstructionType.Drop, [], []),

            new Instruction(InstructionType.LoadLocal, [i], []),
            new Instruction(InstructionType.PushConst, [1], []),
            new Instruction(InstructionType.Add, [], []),
            new Instruction(InstructionType.SetLocal, [i], []),
            new Instruction(InstructionType.Br, [WqValue.Int(2)], []),

            new Instruction(InstructionType.PushConst, [WqValue.Null], []),
            new Instruction(InstructionType.Ret, [], [])
        ]);

        var getStringToOutput = new WqFuncDeclData("getStringToOutput",
        [
            new Instruction(InstructionType.PushConst, ["Hello, World!"], []),
            new Instruction(InstructionType.Ret, [], [])
        ]);

        var sw = Stopwatch.StartNew();
        var results = new Engine().Start([main, getStringToOutput]);
        Console.WriteLine(sw.ElapsedMilliseconds);
        Console.WriteLine(string.Join(", ", results));
    }

    private static nint GetPtr() =>
        typeof(Test).GetMethod(nameof(Test.Print))!.MethodHandle.GetFunctionPointer();
}

public static class Test
{
    public static WqValue Print(WqValue wqValue)
    {
        Console.WriteLine(wqValue);
        return WqValue.Null;
    }
}