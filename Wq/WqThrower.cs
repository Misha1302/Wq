﻿namespace Wq;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Wq.Interpreter;
using Wq.Value;

public static class WqThrower
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    private static T Throw<T>(string s) => throw new InvalidOperationException(s);


    public static T ThrowCannotGetType<T>(WqType realType) =>
        Throw<T>($"Cannot get {typeof(T)}, 'cause the type is {realType}");

    public static T ThrowNotImplemented<T>() =>
        Throw<T>("Not implemented");

    public static T ThrowUnknownType<T>(WqType valueType) =>
        Throw<T>($"Unknown type {valueType}");

    public static T ThrowCannotCompare<T>(WqType aType, WqType bType) =>
        Throw<T>($"Cannot compare {aType} and {bType}");

    public static T ThrowInvalidArgsCount<T>(int current, int need) =>
        Throw<T>($"Count of args is {current} but must be {need}");

    public static T CannotOperateTypes<T>(WqType aT, WqType bT, [CallerMemberName] string methodName = "") =>
        Throw<T>($"Cannot {methodName} {aT} and {bT}");

    public static T ThrowNoKey<T>(T key) =>
        Throw<T>($"Key {key} does not exist");

    public static T ThrowInvalidType<T>() =>
        Throw<T>("Value had invalid type");

    public static T ThrowInvalidInstruction<T>(Instruction instruction)=>
        Throw<T>($"Invalid instruction {instruction}");
}