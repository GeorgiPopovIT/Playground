//Collection expressions
List<int> arr1 = [1, 2, 3, 4, 5, 6];
List<int> arr2 = [7, 8, 9, 10];
int[] arr3 = [.. arr1, .. arr2];
Console.WriteLine(string.Join(", ", arr3));


class Person(string name, int age)
{
    public string Name => name;
    public int Age => age;
}

