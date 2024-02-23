Potion potion = Potion.Water;
bool brewing = true;
string[] ingredients = Enum.GetNames(typeof(Ingredient));
Console.WriteLine($"Starting with {potion}.");

do
{
    Console.Write("Your options are ");

    for (int i = 1; i < ingredients.Length; i++)
    {
        if (i == ingredients.Length - 1) Console.WriteLine($"{ingredients[i]}.");
        else Console.Write($"{ingredients[i]}, ");
    }

    Console.Write("What do you want to add: ");
    string? input = Console.ReadLine();

    Ingredient chosenIngredient = MatchIngredient(input);
    potion = MakePotion(potion, chosenIngredient);
    Console.WriteLine($"You made a {potion} potion.");
    if (potion == Potion.Ruined)
    {
        Console.Write("Start from scratching? (y/n) ");
        input = Console.ReadLine();
        if (input == "y")
        {
            potion = Potion.Water;
            Console.WriteLine($"Starting with {potion}.");
            continue;
        }
        else if (input == "n")
        {
            break;
        }
    }

    Console.Write("Do you want to continue brewing? (y/n) ");
    input = Console.ReadLine();
    brewing = input == "y";
} while (brewing);

Ingredient MatchIngredient(string? input)
{
    return input switch
    {
        "Stardust" or "stardust" => Ingredient.Stardust,
        "SnakeVenom" or "Stake Venom" or "snake venom" => Ingredient.SnakeVenom,
        "DragonBreath" or "Dragon Breath" or "dragon breath" => Ingredient.DragonBreath,
        "ShadowGlass" or "Shadow Glass" or "shadow glass" => Ingredient.ShadowGlass,
        "EyeshineGem" or "Eyeshine Gem" or "eyeshine gem" => Ingredient.EyeshineGem,
        _ => Ingredient.None,
    };
}

Potion MakePotion(Potion currentPotion, Ingredient ingredient)
{
    return (currentPotion, ingredient) switch
    {
        (Potion.Water, Ingredient.Stardust) => Potion.Elixir,
        (Potion.Elixir, Ingredient.SnakeVenom) => Potion.Poison,
        (Potion.Elixir, Ingredient.DragonBreath) => Potion.Flying,
        (Potion.Elixir, Ingredient.ShadowGlass) => Potion.Invisibility,
        (Potion.Elixir, Ingredient.EyeshineGem) => Potion.NightSight,
        (Potion.NightSight, Ingredient.ShadowGlass) => Potion.CloudyBrew,
        (Potion.Invisibility, Ingredient.EyeshineGem) => Potion.CloudyBrew,
        (Potion.CloudyBrew, Ingredient.Stardust) => Potion.Wraith,
        _ => Potion.Ruined,
    };
}

public enum Ingredient { None, Stardust, SnakeVenom, DragonBreath, ShadowGlass, EyeshineGem };
public enum Potion { Water, Elixir, Poison, Flying, Invisibility, NightSight, CloudyBrew, Wraith, Ruined };
