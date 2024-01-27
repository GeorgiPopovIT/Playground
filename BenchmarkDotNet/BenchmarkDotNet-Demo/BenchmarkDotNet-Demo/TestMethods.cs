using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNet_Demo;

[MemoryDiagnoser(false)]
public class TestMethods
{

    [Benchmark]
    [Arguments("IngreDientS", "inGreDientS")]
    public bool Test2(string str1, string str2)
       => str1.ToUpper() == str2.ToUpper();
    [Benchmark]
    [Arguments("IngreDientS", "inGreDientS")]
    public bool Test1(string str1, string str2)
        => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

}
