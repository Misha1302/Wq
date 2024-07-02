namespace Wq.WqValue;

public enum WqType : long
{
    Null = 1,
    Double,
    String,
    Class,
    SharpObject,
    Bool,
    Func,

    Invalid = ~(Null | Double | String | SharpObject)
}