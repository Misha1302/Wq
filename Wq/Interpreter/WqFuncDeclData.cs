namespace Wq.Interpreter;

using static InstructionType;

public record WqFuncDeclData(string Name, Instruction[] Instructions)
{
    public readonly int LocalsCount = Instructions.Max(x =>
        x.InstructionType is LoadLocal or SetLocal ? x.Parameters[0].UnsafeGet<int>() : -1
    ) + 1;
}