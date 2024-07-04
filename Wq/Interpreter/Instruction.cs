namespace Wq.Interpreter;

using Wq.WqValue;

public record Instruction(InstructionType InstructionType, WqValue[] Parameters, WqValue[] DebugInfo);