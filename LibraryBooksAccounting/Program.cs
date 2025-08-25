using LibraryBooksAccounting;
using LibraryBooksAccounting.Routes;

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
                    
                    AuthService authService = new AuthService();
                    var user = authService.AuthMenu();
                    if (user == null) return;
        
                    Console.Clear();
                    RolesRouter.Route(user.Role);
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