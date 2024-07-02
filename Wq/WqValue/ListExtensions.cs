namespace Wq.WqValue;

public static class ListExtensions
{
    public static int BinSearchRec(this List<long> values, long hash, int left, int right)
    {
        if (left > right)
            return ~left;

        var mid = (left + right) / 2;
        var value = values[mid];

        // ReSharper disable TailRecursiveCall
        if (value < hash) return values.BinSearchRec(hash, mid + 1, right);
        if (value > hash) return values.BinSearchRec(hash, left, mid - 1);
        return mid;
    }
}