using System.Text.RegularExpressions;

namespace LibraryBooksAccounting;

public class ValidationService
{
    private UserService _userService = new UserService("../../.././DB/users.json");
    public bool IsValid { get; private set; }
    public string ErrorMessage { get; private set; }
    
    
    public void ValidateLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-RequiredField");
            IsValid = false;
            return;
        }

        if (_userService.UserExists(login))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-UserAlreadyExist");
            IsValid = false;
            return;
        }

        if (!Regex.IsMatch(login, @"^[a-zA-Z0-9_]{3,20}$"))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-LoginRequirements");
            IsValid = false;
            return;
        }

        IsValid = true;
    }

    public void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-RequiredField");
            IsValid = false;
            return;
        }
        
        if (!Regex.IsMatch(name, @"^[А-Яа-яЁёA-Za-z]{2,15}(-[А-Яа-яЁёA-Za-z]{2,15})?$"))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-NameRequirements");
            IsValid = false;
            return;
        }

        IsValid = true;
        
    }
    
    public void ValidatePassword(string pass)
    {
        if (!Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,30}$"))
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-PassRequirements");
            IsValid = false;
            return;
        }

        IsValid = true;
        
    }
    public void ValidatePassword(string pass, string repass)
    {
        if (pass != repass)
        {
            ErrorMessage = LanguageService.GetPhraseByKey("Error-PasswordsDoNotMatch");
            IsValid = false;
            return;
        }

        IsValid = true;
        
    }

    public void ValidateRole(string role)
    {
        int IntRole;
        try
        {
           IntRole = Convert.ToInt32(role);
        }
        catch (Exception)
        {
            IsValid = false;
            ErrorMessage = LanguageService.GetPhraseByKey("Error-InvalidRoleNumber");
            return;
        }

        if (IntRole >= Enum.GetValues(typeof(Roles)).Length || IntRole <= 0)
        {
            IsValid = false;
            ErrorMessage = LanguageService.GetPhraseByKey("Error-InvalidRoleNumber");
        }
        else
        {
            ErrorMessage = "";
            IsValid = true;
        }
    }
}