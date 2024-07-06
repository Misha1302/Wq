namespace Wq.Interpreter;

using Wq.Value;

public class WqFuncFrameArrayPool
{
    private readonly Pool<WqValue[]> _16ArrayPool = new(1024, () => new WqValue[16]);
    private readonly Pool<WqValue[]> _128ArrayPool = new(1024, () => new WqValue[128]);

    public WqValue[] Rent(int minimumLen)
    {
        return minimumLen switch
        {
            <= 16 => _16ArrayPool.Rent(),
            <= 128 => _128ArrayPool.Rent(),
            _ => WqThrower.ThrowTooBigArrayToPool<WqValue[]>()
        };
    }

    public void Return(WqValue[] arrayToReturn)
    {
        switch (arrayToReturn.Length)
        {
            case 16:
                _16ArrayPool.Return(arrayToReturn);
                break;
            case 128:
                _128ArrayPool.Return(arrayToReturn);
                break;
            default:
                WqThrower.ThrowInvalidArrayToReturnIntoPool<object>();
                break;
        }
    }
}