namespace Wq.Interpreter;

using System.Runtime.CompilerServices;
using Wq.Interpreter.Exceptions;
using Wq.Value;
using Wq.Value.Helpers;

public class InstructionExecutor(InterpreterData data)
{
    private Instruction CurInstr => data.Instructions[data.FramesManager.CurFrame.Ip];

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ExecuteInstruction()
    {
        switch (CurInstr.InstructionType)
        {
            case InstructionType.InvalidInstruction:
                InvalidInstruction();
                break;
            case InstructionType.PushConst:
                PushConst();
                break;
            case InstructionType.Drop:
                Drop();
                break;
            case InstructionType.Dup:
                Dup();
                break;
            case InstructionType.Br:
                Br();
                break;
            case InstructionType.BrTrue:
                BrTrue();
                break;
            case InstructionType.BrFalse:
                BrFalse();
                break;
            case InstructionType.BrGt:
                BrGt();
                break;
            case InstructionType.BrGe:
                BrGe();
                break;
            case InstructionType.BrLt:
                BrLt();
                break;
            case InstructionType.BrLe:
                BrLe();
                break;
            case InstructionType.BrEq:
                BrEq();
                break;
            case InstructionType.BrNeq:
                BrNeq();
                break;
            case InstructionType.Add:
                Add();
                break;
            case InstructionType.Sub:
                Sub();
                break;
            case InstructionType.Mul:
                Mul();
                break;
            case InstructionType.Div:
                Div();
                break;
            case InstructionType.Pow:
                Pow();
                break;
            case InstructionType.Rem:
                Rem();
                break;
            case InstructionType.IsDivBy:
                break;
            case InstructionType.And:
                And();
                break;
            case InstructionType.Or:
                Or();
                break;
            case InstructionType.Neg:
                Neg();
                break;
            case InstructionType.Gt:
                Gt();
                break;
            case InstructionType.Ge:
                Ge();
                break;
            case InstructionType.Lt:
                Lt();
                break;
            case InstructionType.Le:
                Le();
                break;
            case InstructionType.Eq:
                Eq();
                break;
            case InstructionType.Neq:
                Neq();
                break;
            case InstructionType.Call:
                Call();
                break;
            case InstructionType.CallSharp:
                CallSharp();
                break;
            case InstructionType.Calli:
                break;
            case InstructionType.Ret:
                Ret();
                break;
            case InstructionType.PushTry:
                break;
            case InstructionType.PushCatch:
                break;
            case InstructionType.PushFinally:
                break;
            case InstructionType.PopTry:
                break;
            case InstructionType.PopCatch:
                break;
            case InstructionType.PopFinally:
                break;
            case InstructionType.Throw:
                break;
            case InstructionType.Nop:
                break;
            case InstructionType.SetLocal:
                SetLocal();
                break;
            case InstructionType.LoadLocal:
                LoadLocal();
                break;
            case InstructionType.IncLocal:
                IncLocal();
                break;
            case InstructionType.DecLocal:
                DecLocal();
                break;
            default:
                WqThrower.ThrowInvalidInstruction<WqValue>(CurInstr);
                break;
        }

        if (!data.Halted)
            data.FramesManager.CurFrame.Ip++;
    }

    private void DecLocal()
    {
        data.FramesManager.CurFrame.Locals[CurInstr.Parameters[0].UnsafeGet<int>()]--;
    }

    private void IncLocal()
    {
        data.FramesManager.CurFrame.Locals[CurInstr.Parameters[0].UnsafeGet<int>()]++;
    }


    private void CallSharp()
    {
        var result = data.SharpMediator.Call(
            CurInstr.Parameters[0].UnsafeGet<nint>(),
            CurInstr.Parameters[1].UnsafeGet<int>()
        );
        data.GlobalStack.Push(result);
    }


    private void Call()
    {
        data.FramesManager.AddFrame(data.FunctionDelcs[CurInstr.Parameters[0].UnsafeGet<int>()]);
        data.FramesManager.CurFrame.Ip--;
    }


    private void Ret()
    {
        // no need to care about stack state. Automatically checks that in stack lies only one return value 
        data.FramesManager.ExitFrame();
        if (!data.FramesManager.HasFrame)
            data.Halted = true;
    }


    private void LoadLocal()
    {
        data.GlobalStack.Push(data.FramesManager.CurFrame.Locals[CurInstr.Parameters[0].UnsafeGet<int>()]);
    }


    private void SetLocal()
    {
        data.FramesManager.CurFrame.Locals[CurInstr.Parameters[0].UnsafeGet<int>()] = data.GlobalStack.Pop();
    }


    private void Gt()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Gt(a, b));
    }


    private void Ge()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Ge(a, b));
    }


    private void Lt()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Lt(a, b));
    }


    private void Le()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Le(a, b));
    }


    private void Eq()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Eq(a, b));
    }


    private void Neq()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueComparer.Neq(a, b));
    }


    private void Neg()
    {
        data.GlobalStack.Push(!data.GlobalStack.Pop().UnsafeGet<bool>());
    }


    private void Or()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(a.UnsafeGet<bool>() || b.UnsafeGet<bool>());
    }


    private void And()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(a.UnsafeGet<bool>() && b.UnsafeGet<bool>());
    }


    private void Add()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Add(a, b));
    }


    private void Sub()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Sub(a, b));
    }


    private void Mul()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Mul(a, b));
    }


    private void Div()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Div(a, b));
    }


    private void Pow()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Pow(a, b));
    }


    private void Rem()
    {
        var (a, b) = PopDouble();
        data.GlobalStack.Push(WqValueOperations.Rem(a, b));
    }


    private void BrGt()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Gt(a, b)) Br();
    }


    private (WqValue a, WqValue b) PopDouble()
    {
        var b = data.GlobalStack.Pop();
        var a = data.GlobalStack.Pop();
        return (a, b);
    }


    private void BrGe()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Ge(a, b)) Br();
    }


    private void BrLt()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Lt(a, b)) Br();
    }


    private void BrLe()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Le(a, b)) Br();
    }


    private void BrEq()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Eq(a, b)) Br();
    }


    private void BrNeq()
    {
        var (a, b) = PopDouble();
        if (WqValueComparer.Neq(a, b)) Br();
    }


    private void BrTrue()
    {
        if (data.GlobalStack.Pop().Get<bool>())
            Br();
    }


    private void BrFalse()
    {
        if (!data.GlobalStack.Pop().Get<bool>())
            Br();
    }


    private void Br()
    {
        data.FramesManager.CurFrame.Ip = CurInstr.Parameters[0].UnsafeGet<int>() - 1;
    }


    private void Dup()
    {
        data.GlobalStack.Dup();
    }


    private void Drop()
    {
        data.GlobalStack.Drop();
    }


    private void PushConst()
    {
        data.GlobalStack.Push(CurInstr.Parameters[0]);
    }


    private void InvalidInstruction()
    {
        throw new InvalidInstructionException();
    }
}