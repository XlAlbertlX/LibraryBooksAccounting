namespace LibraryBooksAccounting;

public class InputValuesService
{
    private ValidationService _validationService = new ValidationService();
    
    public string RegInputLogin()
    {
        string login = "";
        do
        {

            LanguageService.Print("LoginInput");
            login = Console.ReadLine();

            _validationService.ValidateLogin(login);

            if (!_validationService.IsValid)
            {
                FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
            }
        } while (!_validationService.IsValid);
        return login.ToLower();
    }
    
    public string InputName(bool AllowEmpty = false)
    {
        string name = "";
        
        do
        {
            
            LanguageService.Print("NameInput");
            name = Console.ReadLine();
            _validationService.ValidateName(name, AllowEmpty);

            if (!_validationService.IsValid)
            {
                FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
            }
        } while (!_validationService.IsValid);
        return name;
    }
    
    public string InputSurname(bool AllowEmpty = false)
    {
        string name = "";
       
        do
        {
            LanguageService.Print("SurnameInput");
            name = Console.ReadLine();
            _validationService.ValidateName(name, AllowEmpty);

            if (!_validationService.IsValid)
            {
                FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
            }
        } while (!_validationService.IsValid);
        return name;
    }
    
    public string InputPassword(bool AllowEmpty = false)
    {
        string pass = "";
        string repass = "";
       
        
        do
        {

            LanguageService.Print("PasswordInput");
            pass = Console.ReadLine();
            _validationService.ValidatePassword(pass, AllowEmpty);
            if (!_validationService.IsValid)
            {
                FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
            }
            
        } while (!_validationService.IsValid);
        
        do
        {
            LanguageService.Print("RePasswordInput");
            repass = Console.ReadLine();
            _validationService.ValidatePassword(pass, repass);
            if (!_validationService.IsValid)
            {
                FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
            }
            
        } while (!_validationService.IsValid);
        
        return pass;
    }
    public Roles ChooseRole()
    {
        Roles role;
        string localRole = "";
        do
        {

            LanguageService.PrintLine("ChooseRole");
            for (int i = 1; i < Enum.GetValues<Roles>().Length; i++)
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(Roles), i)}");
            }

            try
            {
                localRole = Console.ReadLine();
                _validationService.ValidateRole(localRole);
           
                if (!_validationService.IsValid)
                {
                    FormattedOutput.Print(_validationService.ErrorMessage, $"{ConsoleColor.Red}");
                }
            }
            catch
            {
                FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-InvalidValue"), $"{ConsoleColor.Red}");
            }
            
        } while (!_validationService.IsValid);
        
        role = (Roles)Convert.ToInt32(localRole);
        return role;
    }
}