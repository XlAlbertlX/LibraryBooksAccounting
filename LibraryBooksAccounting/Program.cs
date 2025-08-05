using LibraryBooksAccounting;

namespace LibraryBooksAccounting;


class Program {
    public static void Main(string[] args)
    { 
        LanguageService.Init(Languages.English);
        MainMenu();
        
    }

    public static void MainMenu()
    {
        while (true)
        {
            int menuSection = MainMenuChooseOptions();
            switch (menuSection)
            {
                case 1:
                    User user = new User();
                    AuthService authService = new AuthService();
                    user = authService.AuthMenu();
                    if (user == null) return;
        
                    Console.Clear();
                    Console.WriteLine($"Добро пожаловать на главный экран, {user.Name}, ваша роль {user.Role}!");
                    break;
                case 2:
                {
                    LanguageService.ChooseLanguage();
                    break;
                }
            }
        }
    }
    private static int MainMenuChooseOptions()
    {
        Console.Clear();
        LanguageService.PrintLine("MainMenu");
        int result = 0;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Clear();
            LanguageService.PrintLine("MainMenu");
        }
        return result;
    }
}