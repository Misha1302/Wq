namespace Wq.Interpreter;

using Wq.Value;

public record Instruction(InstructionType InstructionType, WqValue[] Parameters, WqValue[] DebugInfo)
{
    public override string ToString() => $"{InstructionType} [{string.Join(", ", Parameters)}]";
}