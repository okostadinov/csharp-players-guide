while (true) {
    System.Console.Write("Enter password: ");
    string? input = Console.ReadLine();

    if (input != null) {
        bool valid = PasswordValidator.Valid(input);
        System.Console.WriteLine($"Password is {valid}");
    }
}

static class PasswordValidator
{
    public static bool Valid(string password)
    {
        return ValidLength(password) && ContainsRequiredCharacters(password) && ExcludesSpecifiedCharacters(password); 
    }

    private static bool ValidLength(string password)
    {
        return password.Length > 5 && password.Length < 14;
    }

    private static bool ContainsRequiredCharacters(string password)
    {
        bool hasUpper = false;
        bool hasLower = false;
        bool hasNumber = false;

        foreach (char letter in password)
        {
            if (char.IsUpper(letter))
                hasUpper = true;
            else if (char.IsLower(letter))
                hasLower = true;
            else if (char.IsDigit(letter))
                hasNumber = true;
        }

        return hasUpper && hasLower && hasNumber;
    }

    private static bool ExcludesSpecifiedCharacters(string password)
    {
        return !(password.Contains('T') || password.Contains('&'));
    }
}