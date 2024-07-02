namespace Wq.WqValue;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Wq.WqValue.Helpers;

[StructLayout(LayoutKind.Explicit)]
[SkipLocalsInit]
[DebuggerDisplay("{ToDebugString}")]
public readonly struct WqValue
{
    public override bool Equals(object? obj) => obj is WqValue other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(_double, _string, _class, _sharpObject, (int)Type);

    public long Hash => WqValueHelper.Hash(_double, _sharpObject, Type);

    private const int ValueOffset = 0;
    private const int RefOffset = 8;
    private const int TypeOffset = 16;
    
    public static readonly WqValue Null = new();


    [FieldOffset(ValueOffset)] private readonly bool _bool;
    [FieldOffset(ValueOffset)] private readonly double _double;

    [FieldOffset(RefOffset)] private readonly string _string;
    [FieldOffset(RefOffset)] private readonly WqClass _class;
    [FieldOffset(RefOffset)] private readonly WqFunc _func;
    [FieldOffset(RefOffset)] private readonly object _sharpObject;

    [FieldOffset(TypeOffset)] public readonly WqType Type;

    public WqValue()
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _double = 0;
        _sharpObject = null!;
        Type = WqType.Null;
    }

    public WqValue(bool d)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _bool = d;
        _sharpObject = null!;
        Type = WqType.Double;
    }

    public WqValue(double d)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _double = d;
        _sharpObject = null!;
        Type = WqType.Double;
    }

    public WqValue(string s)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _string = s;
        _double = 0;
        Type = WqType.String;
    }

    public WqValue(object o)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _sharpObject = o;
        _double = 0;
        Type = WqType.SharpObject;
    }

    public WqValue(WqClass c)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _class = c;
        _double = 0;
        Type = WqType.Class;
    }

    public WqValue(WqFunc f)
    {
        WqValueHelper.Skip(out _double, out _string, out _class, out _sharpObject, out _bool, out _func);
        _func = f;
        _double = 0;
        Type = WqType.Func;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>()
    {
        if (typeof(T) == typeof(double) && Type == WqType.Double) return _double.As<double, T>();
        if (typeof(T) == typeof(bool) && Type == WqType.Bool) return _bool.As<bool, T>();
        if (typeof(T) == typeof(string) && Type == WqType.String) return _string.As<string, T>();
        if (typeof(T) == typeof(object) && Type == WqType.SharpObject) return _sharpObject.As<object, T>();
        if (typeof(T) == typeof(WqClass) && Type == WqType.Class) return _class.As<WqClass, T>();
        if (typeof(T) == typeof(WqFunc) && Type == WqType.Func) return _func.As<WqFunc, T>();

        return WqThrower.ThrowCannotGetType<T>(Type);
    }

    public string ToDebugString() => WqValueFormatter.ToDebugString(this);
    public override string ToString() => WqValueFormatter.ToStdString(this);


    public bool IsNull() => Type == WqType.Null;
    public bool Equals(WqValue other) => WqValueComparer.Eq(this, other);


    public static bool operator ==(WqValue a, WqValue b) => WqValueComparer.Eq(a, b);
    public static bool operator !=(WqValue a, WqValue b) => WqValueComparer.Neq(a, b);
    public static bool operator <(WqValue a, WqValue b) => WqValueComparer.Lt(a, b);
    public static bool operator >(WqValue a, WqValue b) => WqValueComparer.Gt(a, b);
    public static bool operator <=(WqValue a, WqValue b) => WqValueComparer.Le(a, b);
    public static bool operator >=(WqValue a, WqValue b) => WqValueComparer.Ge(a, b);

    public static WqValue operator ++(WqValue a) => a + 1;
    public static WqValue operator --(WqValue a) => a - 1;

    public static WqValue operator +(WqValue a, WqValue b) => WqValueOperations.Add(a, b);
    public static WqValue operator -(WqValue a, WqValue b) => WqValueOperations.Sub(a, b);
    public static WqValue operator *(WqValue a, WqValue b) => WqValueOperations.Mul(a, b);
    public static WqValue operator /(WqValue a, WqValue b) => WqValueOperations.Div(a, b);
    public static WqValue operator ^(WqValue a, WqValue b) => WqValueOperations.Pow(a, b); // power!

    public static implicit operator WqValue(double value) => new(value);
    public static implicit operator WqValue(string value) => new(value);
    public static implicit operator WqValue(bool value) => new(value);
}