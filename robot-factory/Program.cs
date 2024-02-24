using System.Dynamic;

int count = 1;
while (true)
{
    dynamic robot = new ExpandoObject();
    robot.ID = 1;

    Console.Write("Does the robot have a name (yes/no)? ");
    string? input = Console.ReadLine();
    if (input == "yes")
    {
        Console.Write("Enter a name: ");
        robot.Name = Console.ReadLine();
    }

    Console.Write("Does the robot have a specific size (yes/no)? ");
    input = Console.ReadLine();
    if (input == "yes")
    {
        Console.Write("Enter height: ");
        robot.Height = Console.ReadLine();
        Console.Write("Enter width: ");
        robot.Width = Console.ReadLine();
    }

    Console.Write("Does the robot have a specific color (yes/no)? ");
    input = Console.ReadLine();
    if (input == "yes")
    {
        Console.Write("Enter color: ");
        robot.Color = Console.ReadLine();
    }

    DisplayRobotCharacteristics(robot);
    count++;
}

void DisplayRobotCharacteristics(dynamic robot)
{
    var robotDictionary = (IDictionary<string, object>)robot;

    foreach (string key in robotDictionary.Keys)
        Console.WriteLine($"{key}: {robotDictionary[key]}");
}