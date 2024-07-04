namespace Wq.Value;

public readonly partial struct WqValue
{
    private WqValue(int value)
    {
        _i64 = value;
        _obj = null!;
        Type = WqType.Internal;
    }

    private WqValue(nint value)
    {
        _i64 = value;
        _obj = null!;
        Type = WqType.Internal;
    }

    public static WqValue Int(int i) => new(i);
    public static WqValue Nint(nint i) => new(i);
}