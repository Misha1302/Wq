namespace Wq.Value;

public class WqClass(int capacity, List<WqClass> parents)
{
    private const int DefaultCapacity = 7;

    private readonly List<long> _hashes = new(capacity);
    private readonly List<WqValue> _values = new(capacity);

    public WqClass() : this(DefaultCapacity, []) { }

    public void Add(WqValue key, WqValue wqValue)
    {
        var hashCode = key.Hash;

        _hashes.Add(hashCode);
        _hashes.Sort();

        _values.Insert(_hashes.BinSearchRec(hashCode, 0, _hashes.Count), wqValue);
    }

    public void Remove(WqValue key)
    {
        var ind = Index(key, out var instance, false);
        instance._hashes.RemoveAt(ind);
        instance._values.RemoveAt(ind);
    }

    public bool Has(WqValue key) => Index(key, out _, false) >= 0;

    public WqValue Get(WqValue key)
    {
        TryGet(key, out var result, true);
        return result;
    }

    public bool TryGet(WqValue key, out WqValue wqValue, bool throwEx = false)
    {
        var ind = Index(key, out var instance, throwEx);

        if (ind < 0)
            return CannotGet(out wqValue);

        wqValue = instance._values[ind];
        return true;
    }

    private static bool CannotGet(out WqValue wqValue)
    {
        wqValue = WqValue.Null;
        return false;
    }

    private int Index(WqValue key, out WqClass instance, bool throwEx)
    {
        var ind = _hashes.BinSearchRec(key.Hash, 0, _hashes.Count);
        if (ind < 0)
            return IndexInParents(key, out instance, ind, throwEx);

        instance = this;
        return ind;
    }

    private int IndexInParents(WqValue key, out WqClass instance, int ind, bool throwEx)
    {
        for (var i = parents.Count - 1; i >= 0; i--)
        {
            ind = parents[i].Index(key, out instance, false);
            if (ind >= 0)
                return ind;
        }

        if (throwEx)
            WqThrower.ThrowNoKey(key);

        instance = null!;
        return ind;
    }
}