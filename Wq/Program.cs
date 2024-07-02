namespace Wq;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(new WqValue.WqValue(54) == new WqValue.WqValue(3.43) == new WqValue.WqValue(false));
    }
}