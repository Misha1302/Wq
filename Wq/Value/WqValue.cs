namespace Wq.Value;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Wq.Value.Helpers;

[DebuggerDisplay("{ToDebugString()}")]
public readonly partial struct WqValue
{
    public override bool Equals(object? obj) => obj is WqValue other && Equals(other);
    public override int GetHashCode() => Hash.GetHashCode();

    public long Hash => WqValueHelper.Hash(_i64.As<long, double>(), _obj, Type);

    public static readonly WqValue Null = new();


    private readonly long _i64;
    private readonly object _obj;
    public readonly WqType Type;


    public WqValue()
    {
        _i64 = 0;
        _obj = null!;
        Type = WqType.Null;
    }

    public WqValue(bool d)
    {
        _i64 = d.As<bool, long>();
        _obj = null!;
        Type = WqType.Bool;
    }

    public WqValue(double d)
    {
        _i64 = d.As<double, long>();
        _obj = null!;
        Type = WqType.Double;
    }

    public WqValue(string s)
    {
        _obj = s;
        _i64 = 0;
        Type = WqType.String;
    }

    public WqValue(object o)
    {
        _obj = o;
        _i64 = 0;
        Debug.Print("Object ctor used. It's not a mistake?");
        Type = WqType.SharpObject;
    }

    public WqValue(WqClass c)
    {
        _obj = c;
        _i64 = 0;
        Type = WqType.Class;
    }

    public WqValue(WqFunc f)
    {
        _obj = f;
        _i64 = 0;
        Type = WqType.Func;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>()
    {
        if (typeof(T) == typeof(double) && Type == WqType.Double) return _i64.As<long, T>();
        if (typeof(T) == typeof(bool) && Type == WqType.Bool) return _i64.As<long, T>();
        if (typeof(T) == typeof(string) && Type == WqType.String) return (T)_obj;
        if (typeof(T) == typeof(object) && Type == WqType.SharpObject) return (T)_obj;
        if (typeof(T) == typeof(WqClass) && Type == WqType.Class) return (T)_obj;
        if (typeof(T) == typeof(WqFunc) && Type == WqType.Func) return (T)_obj;

        return WqThrower.ThrowCannotGetType<T>(Type);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T UnsafeGet<T>()
    {
        if (typeof(T).IsValueType)
            return _i64.As<long, T>();
        return (T)_obj;
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