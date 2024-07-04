namespace Wq.Interpreter;

public enum InstructionType
{
    InvalidInstruction,

    PushConst,
    Drop,
    Dup,

    Br,
    BrTrue,
    BrFalse,
    BrGt,
    BrGe,
    BrLt,
    BrLe,
    BrEq,
    BrNeq,

    Add,
    Sub,
    Mul,
    Div,
    Pow,
    Rem,
    IsDivBy,

    And,
    Or,
    Neg,

    Gt,
    Ge,
    Lt,
    Le,
    Eq,
    Neq,

    Call,
    CallSharp,
    Calli,

    Ret,

    PushTry,
    PushCatch,
    PushFinally,

    PopTry,
    PopCatch,
    PopFinally,

    Throw,

    Nop,

    SetLocal,
    LoadLocal,
    IncLocal,
    DecLocal
}