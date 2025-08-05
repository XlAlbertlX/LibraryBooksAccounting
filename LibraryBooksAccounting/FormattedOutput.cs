namespace LibraryBooksAccounting;

public class FormattedOutput
{
    public static void Print(string output, string color)
    {
        switch (color)
        {
            case "Red":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "Green":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
        }
        Console.WriteLine(output);
        Console.ResetColor();
    }
}