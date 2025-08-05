using System.Text.Json;

namespace LibraryBooksAccounting;

public class LanguageService
{
    private static Dictionary<string, string> PhrasesPack = new Dictionary<string, string>();

    public static void Init(Languages language)
    {
        switch (language)
        {
            case Languages.Russian:
            {
                string path = "../../.././Languages/ru.json";
                var json = File.ReadAllText(path);
                PhrasesPack = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                break;
            }
            case Languages.English:
            {
                string path = "../../.././Languages/en.json";
                var json = File.ReadAllText(path);
                PhrasesPack = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                break; 
            }
                
        }
    }

    public static void ChooseLanguage()
    {
        Console.Clear();
        Console.WriteLine("Please choose language: ");
        for (int i = 0; i < Enum.GetValues(typeof(Languages)).Length; i++)
        {
            Console.WriteLine($"{i} - {Enum.GetName(typeof(Languages), i)}");
        }

        int choice;
        while (true)
        {
            string input = Console.ReadLine();

            if (!int.TryParse(input, out choice))
            {
                FormattedOutput.Print(GetPhraseByKey("Error-WrongNumberFromList"), $"{ConsoleColor.Red}");
            }
            else if (choice < 0 || choice >= Enum.GetValues(typeof(Languages)).Length)
            {
                FormattedOutput.Print(GetPhraseByKey("Error-WrongNumberFromList"), $"{ConsoleColor.Red}");
            }
            else
            {
                break;
            }

            Console.Write(GetPhraseByKey("TryAgain"));
        }
        
        Init((Languages)choice);
    }
    public static string GetPhraseByKey(string key)
    {
        return PhrasesPack[key];
    }

    public static void Print(string key)
    {
        Console.Write(GetPhraseByKey(key));
    }
    public static void PrintLine(string key)
    {
        Console.WriteLine(GetPhraseByKey(key));
    }
}