int[] numbers = [1, 9, 2, 8, 3, 7, 4, 6, 5];

IEnumerable<int> result = ProceduralQuery(numbers);
foreach (int number in result) Console.Write(number + " ");
Console.WriteLine();

IEnumerable<int> result2 = KeywordBasedQuery(numbers);
foreach (int number in result2) Console.Write(number + " ");
Console.WriteLine();

IEnumerable<int> result3 = MethodCallBasedQuery(numbers);
foreach (int number in result3) Console.Write(number + " ");
Console.WriteLine();

IEnumerable<int> ProceduralQuery(int[] numbers)
{
    numbers = Array.FindAll(numbers, n => n % 2 == 0);
    Array.Sort(numbers);

    int[] result = new int[numbers.Length];

    for (int i = 0; i < numbers.Length; i++)
    {
        result[i] = numbers[i] * 2;
    }

    return result;
}

IEnumerable<int> KeywordBasedQuery(int[] numbers)
{
    return from n in numbers
           where n % 2 == 0
           orderby n
           select n * 2;
}

IEnumerable<int> MethodCallBasedQuery(int[] numbers)
{
    return numbers
           .Where(n => n % 2 == 0)
           .Order()
           .Select(n => n * 2);
}