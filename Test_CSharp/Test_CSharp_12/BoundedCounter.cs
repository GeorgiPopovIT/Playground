namespace Test_CSharp_12;

internal class BoundedCounter(int initValue)
{
    private const int MAX_VALUE = 5000;
    private int _initValue = initValue;

    public int Value => _initValue;

    public void TryIncrement()
    {
        if (Value < MAX_VALUE)
        {
            Interlocked.Increment(ref _initValue);
            Console.WriteLine($"True V:{Value}, v:{_initValue}");
            return;
        }
        Console.WriteLine($"----------False, V:{Value}, v:{_initValue}");
    }
}
