namespace Wq.Value;

public record WqFunc
{
    public WqValue Call(WqValue wqValue, WqValue value1) => throw new NotImplementedException();

    public WqValue Call(WqValue wqValue) => throw new NotImplementedException();
}