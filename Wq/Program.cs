namespace Wq;

using Wq.Interpreter;
using Wq.Value;

public static class Program
{
    public static void Main(string[] args)
    {
        var main = new WqFuncDeclData("main",
        [
            new Instruction(InstructionType.Call, [WqValue.Int(1)], []),
            new Instruction(InstructionType.CallSharp, [WqValue.Nint(GetPtr()), WqValue.Int(1)], []),
            new Instruction(InstructionType.Ret, [], [])
        ]);

        var getStringToOutput = new WqFuncDeclData("getStringToOutput",
        [
            new Instruction(InstructionType.PushConst, ["Hello, World!"], []),
            new Instruction(InstructionType.Ret, [], [])
        ]);

        var results = new Engine().Start([main, getStringToOutput], LaunchMode.Debug);
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