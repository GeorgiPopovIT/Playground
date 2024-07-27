using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet_Demo;

//| Method           | Mean          | Error      | StdDev     | Gen0   | Allocated |
//|----------------- |--------------:|-----------:|-----------:|-------:|----------:|
//| ArrayWithoutSpan | 6,570.1745 ns | 30.6238 ns | 27.1472 ns | 4.8218 |   10120 B |
//| ArrayAsSpan      |     0.7637 ns |  0.0002 ns |  0.0001 ns |      - |         - |
//| SliceString      | 1,463.5699 ns |  5.2407 ns |  4.9022 ns | 0.0381 |      80 B |
//| SliceSpan        |     1.9516 ns |  0.0130 ns |  0.0108 ns |      - |         - |
[MemoryDiagnoser]
public class TestMethods
{
    private int[] arr;
    private readonly int size = 10000;

    [GlobalSetup]
    public void Initialize()
    {
        this.arr = new int[size];
        for (int i = 0; i < this.size; i++)
        {
            arr[i] = i;
        }
    }
    [Benchmark]
    [Arguments("IngreDientS", "inGreDientS")]
    public bool Test2(string str1, string str2)
      => str1.ToUpper() == str2.ToUpper();
    [Benchmark]
    [Arguments("IngreDientS", "inGreDientS")]
    public bool Test1(string str1, string str2)
        => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

    [Benchmark]
    public int[] ArrayWithoutSpan()
    {
        var sliced = arr.Skip(size / 2).Take(size / 4).ToArray();

        return sliced;
    }

    [Benchmark]
    public Span<int> ArrayAsSpan()
    {
        var sliced = arr.AsSpan().Slice(size / 2, size / 4);

        return sliced;
    }

    [Benchmark]
    public void SliceString()
    {
        string input = "Hello, how are you";
        int commaPos = input.IndexOf(",");
        string first = input.Substring(0, commaPos);
        string second = input.Substring(commaPos + 1);
    }

    [Benchmark]
    public void SliceSpan()
    {
        string input = "Hello, how are you";
        ReadOnlySpan<char> inputSpan = input;
        int commaPos = input.IndexOf(',');
        ReadOnlySpan<char> first = inputSpan.Slice(0, commaPos);
        ReadOnlySpan<char> second = inputSpan.Slice(commaPos + 1);
    }
}