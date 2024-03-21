using System.Buffers;

List<int> arr1 = [1, 2, 3, 4, 5, 6];
List<int> arr2 = [7, 8, 9, 10];
int[] arr3 = [.. arr1, .. arr2];
Console.WriteLine(string.Join(", ", arr3));

//SearchValues<int> searchValues = SearchValues.Create();


