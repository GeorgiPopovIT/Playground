List<int> list = [1,2,3,4,5,6,7,8,9,10,1,2,3];

var countElements = list.CountBy(x => x);

foreach (var element in countElements)
{
    Console.WriteLine($"There are {element.Value} numbers with the value {element.Key}");
}

var dict = new Dictionary<string, int>
{
    { "foo", 10 },
    { "bar", 20 },
    { "baz", 30 }
};

var lookup = dict.GetAlternateLookup<ReadOnlySpan<char>>();

var keys = "foo, bar, baz";

foreach (var range in keys.AsSpan().Split(','))
{
    ReadOnlySpan<char> key = keys.AsSpan(range).Trim();

    int value = lookup[key];
    Console.WriteLine(value);
}
