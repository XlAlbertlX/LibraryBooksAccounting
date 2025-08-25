using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace LibraryBooksAccounting;



public class UserService
{
    private InputValuesService inputValuesService = new InputValuesService();
    private string _DBpath;
    private List<User> _usersList = new();
    public UserService(string DBpath)
    {
        _DBpath = DBpath;
        GetAllUsers();
    }

    public List<User> GetAllUsers()
    {
        if (!File.Exists(_DBpath))
        {
            Save();
            return _usersList;
        }
        
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        string json = File.ReadAllText(_DBpath);
        _usersList = JsonConvert.DeserializeObject<List<User>>(json, settings) ?? new List<User>();
        
        
        return _usersList;
    }

    public bool AddUser(User user)
    {
        if (_usersList.Any(u => u.Login == user.Login))
        {
            return false;
        }
        user.UUID = Guid.NewGuid().ToString();
        _usersList.Add(user);
        Save();
        return true;
    }
    
    public void CreateUser()
    {
        Console.Clear();
        string login = inputValuesService.RegInputLogin();
        if (UserExists(login))
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-UserAlreadyExist"), $"{ConsoleColor.Red}");
            return;
        }
        string name = inputValuesService.InputName();
        string surname = inputValuesService.InputSurname();
        string password = inputValuesService.InputPassword();
        Roles role = inputValuesService.ChooseRole();
        User user = null;
        switch (role)
        {
            case Roles.Reader:
                user = new Reader(login, name, surname, password);
                break;
            case Roles.Admin:
                user = new Admin
                {
                    Login = login,
                    Name = name,
                    Surname = surname,
                    Password = password,
                };
                break;
            case Roles.Librarian:
                user = new Librarian
                {
                    Login = login,
                    Name = name,
                    Surname = surname,
                    Password = password
                };
                break;
        }
        AddUser(user);
    }

    public void DeleteUser(string UUID)
    {
        User user = _usersList.FirstOrDefault(u => u.UUID == UUID);
        if (user == null)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-UserNoExistByUUID"), $"{ConsoleColor.Red}");
            Thread.Sleep(2000);
            return;
        }
        _usersList.Remove(user);
        Save();
    }

    public void EditUser(Admin user)
    {
        _usersList.Remove(user);
        Console.WriteLine("\n");
        
        string name = inputValuesService.InputName(true);
        name = string.IsNullOrEmpty(name) ? user.Name : name;
        string surname = inputValuesService.InputSurname(true);
        surname = string.IsNullOrEmpty(surname) ? user.Surname : surname;
        string password = inputValuesService.InputPassword(true);
        password = string.IsNullOrEmpty(password) ? user.Password : password;
        Roles role = inputValuesService.ChooseRole();
        user.Name = name;
        user.Surname = surname;
        user.Password = password;
        _usersList.Add(user);
        Save();

    }
    public void EditUser(Librarian user)
    {
        _usersList.Remove(user);
        Console.WriteLine("\n");
        
        string name = inputValuesService.InputName(true);
        name = string.IsNullOrEmpty(name) ? user.Name : name;
        string surname = inputValuesService.InputSurname(true);
        surname = string.IsNullOrEmpty(surname) ? user.Surname : surname;
        string password = inputValuesService.InputPassword(true);
        password = string.IsNullOrEmpty(password) ? user.Password : password;
        Roles role = inputValuesService.ChooseRole();
        user.Name = name;
        user.Surname = surname;
        user.Password = password;
        _usersList.Add(user);
        Save();

    }
    public void EditUser(Reader user)
    {
        _usersList.Remove(user);
        Console.WriteLine("\n");
        
        string name = inputValuesService.InputName(true);
        name = string.IsNullOrEmpty(name) ? user.Name : name;
        string surname = inputValuesService.InputSurname(true);
        surname = string.IsNullOrEmpty(surname) ? user.Surname : surname;
        string password = inputValuesService.InputPassword(true);
        password = string.IsNullOrEmpty(password) ? user.Password : password;
        Roles role = inputValuesService.ChooseRole();
        user.Name = name;
        user.Surname = surname;
        user.Password = password;
        _usersList.Add(user);
        Save();
        
    }

    public User GetUserByLogin(string login)
    {
        return _usersList.FirstOrDefault(u => u.Login == login.ToLower());
    }
    public User GetUserByUUID(string UUID)
    {
        return _usersList.FirstOrDefault(u => u.UUID == UUID);
    }
    
    public bool UserExists(string login)
    {
        return _usersList.Any(u => u.Login == login.ToLower());
    }

    private void Save()
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        string json = JsonConvert.SerializeObject(_usersList, settings);
        File.WriteAllText(_DBpath, json);
    }
}