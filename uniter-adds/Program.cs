Console.WriteLine(Add(2, 3));
Console.WriteLine(Add(2.5, 3.5));
Console.WriteLine(Add("Hello", "again"));
Console.WriteLine(Add(DateTime.Now, TimeSpan.FromSeconds(30)));

dynamic Add(dynamic a, dynamic b) => a + b;