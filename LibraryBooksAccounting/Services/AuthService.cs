using System.Text.Json.Serialization;

namespace LibraryBooksAccounting;

public class AuthService
{
    private UserService userService = new UserService("../../.././DB/Users.json");
    private ValidationService validationService = new ValidationService();
    private InputValuesService inputValuesService = new InputValuesService();
    private string? _password;
    private string? _username;
    private AuthMethods _loginMethod;
    Librarian _librarian = new Librarian();
    private bool _loggedIn = false;
    AuthMethods LoginMethod
    {
        get
        {
            return _loginMethod;
        }
        set
        {
            if (value == AuthMethods.login || value == AuthMethods.registration)
            {
                _loginMethod = value;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                LanguageService.PrintLine("Error-WrongNumberFromList");
                Console.ResetColor();
                LanguageService.PrintLine("AuthMenuWelcomeScreen");
            }
        }
    }
    
    public User AuthMenu()
    {
        int methodNumber;
        Console.Clear();
        LanguageService.PrintLine("AuthMenuWelcomeScreen");
 
        while (!int.TryParse(Console.ReadLine(), out methodNumber) || !Enum.IsDefined(typeof(AuthMethods), methodNumber))
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-WrongNumberFromList"), $"{ConsoleColor.Red}");
            LanguageService.Print("TryAgain");
        }
        LoginMethod = (AuthMethods)methodNumber;
        

        switch (LoginMethod)
        {
            case AuthMethods.login:
                {
                    User user = null;
                    while (!_loggedIn)
                    {
                        _loggedIn = Login(out user);
                        bool wultc = false;
                        if (!_loggedIn) wultc = WouldUserLikeToContinue();
                        if (!wultc && !_loggedIn) return null;
                    }

                    return user;
                }

            case AuthMethods.registration:
            {
                User user = null;
                while (!_loggedIn)
                {
                    _loggedIn = Registration(out user);
                }
                return user;
                break;
            }
        }
        return null;
    }
    

    private bool WouldUserLikeToContinue()
    {
        LanguageService.Print("DoYouWantToResume");
        string response = Console.ReadLine();
        if (response.ToLower() == "y" || response.ToLower() == "yes" || response.ToLower() == "да") return true;
        
        return false;
    }
    
    
    public bool Login(out User user)
    {
        Console.Clear();
        LanguageService.PrintLine("Authorization");
        
        LanguageService.Print("LoginInput");
        string login = Console.ReadLine();
        
        var _user = userService.GetUserByLogin(login);
        user = _user;
        if (_user == null)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-NoExistUser"), $"{ConsoleColor.Red}");
            return false;
        }
        LanguageService.Print("PasswordInput");
        string password = Console.ReadLine();
        if (_user.Password != password)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-IncorrectPassword"), $"{ConsoleColor.Red}");
            return false;
        }
        
        
        FormattedOutput.Print(LanguageService.GetPhraseByKey("SuccessfullLogin"), $"{ConsoleColor.Green}");
        Thread.Sleep(2000);
        return true;
    }

    public bool Registration(out User Ruser)
    {
        Console.Clear();
        LanguageService.PrintLine("Registration");
        string login = inputValuesService.RegInputLogin();
        string name = inputValuesService.InputName();
        string surname = inputValuesService.InputSurname();
        string password = inputValuesService.InputPassword();
        int booksCount = 3;
        userService = new UserService("../../.././DB/Users.json");
        Roles role = Roles.Reader;
        
        User user = new Reader(login, name, surname, password);
        Ruser = user;
        userService.AddUser(user);
        
        
        FormattedOutput.Print(LanguageService.GetPhraseByKey("SuccessfullLogin"), $"{ConsoleColor.Green}");
        Thread.Sleep(2000);
        return true;
    }
    
    
}