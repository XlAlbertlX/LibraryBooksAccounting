using System.Text.Json.Serialization;

namespace LibraryBooksAccounting;

public class AuthService
{
    private UserService userService = new UserService("../../.././DB/users.json");
    private ValidationService validationService = new ValidationService();
    private InputValuesService inputValuesService = new InputValuesService();
    private string? _password;
    private string? _username;
    private AuthMethods _loginMethod;
    User user = new User();
    Employee employee = new Employee();
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
                while (!_loggedIn)
                {
                    PasswordLoginInput();
                    _loggedIn = Login();
                    if (!_loggedIn)
                    {
                        bool wultc = WouldUserLikeToContinue();
                        if (wultc) _loggedIn = false;
                        else return null;
                    }
                }

                return user;
                break;
            case AuthMethods.registration:
                while (!_loggedIn)
                {
                    RegistrationDataInput();
                    _loggedIn = Registration();
                }
                return user;
                break;
        }
        
        return null;
    }

    private void PasswordLoginInput()
    {
        Console.Clear();
        LanguageService.PrintLine("Authorization");
        LanguageService.Print("LoginInput");
        user.Login = Console.ReadLine();
        LanguageService.Print("PasswordInput");
        user.Password = Console.ReadLine();
    }

    private void RegistrationDataInput()
    {
        Console.Clear();
        LanguageService.PrintLine("Registration");
        user.Login = inputValuesService.InputLogin();
        user.Name = inputValuesService.InputName();
        user.Surname = inputValuesService.InputSurname();
        user.Password = inputValuesService.InputPassword();
    }

    private bool WouldUserLikeToContinue()
    {
        LanguageService.Print("DoYouWantToResume");
        string response = Console.ReadLine();
        if (response.ToLower() == "y" || response.ToLower() == "yes" || response.ToLower() == "да") return true;
        
        return false;
    }
    
    
    public bool Login()
    {
        
        User _user = userService.GetUserByLogin(user.Login);
        if (_user == null)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-NoExistUser"), $"{ConsoleColor.Red}");
            return false;
        }

        if (_user.Password != user.Password)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-IncorrectPassword"), $"{ConsoleColor.Red}");
            return false;
        }
        
        user = _user;
        FormattedOutput.Print(LanguageService.GetPhraseByKey("SuccessfullLogin"), $"{ConsoleColor.Green}");
        return true;
    }

    public bool Registration()
    {
        userService = new UserService("../../.././DB/users.json");
        user.Role = Roles.Reader;
        
        userService.AddUser(user);
        
        FormattedOutput.Print("Вы успешно прошли регистрацию!", $"{ConsoleColor.Green}");
        return true;
    }
    
    
}