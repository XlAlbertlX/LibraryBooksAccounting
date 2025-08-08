using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace LibraryBooksAccounting;



public class UserService
{
    
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
            Formatting = Formatting.Indented
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

    public User GetUserByLogin(string login)
    {
        return _usersList.FirstOrDefault(u => u.Login == login.ToLower());
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
            Formatting = Formatting.Indented
        };

        string json = JsonConvert.SerializeObject(_usersList, settings);
        File.WriteAllText(_DBpath, json);
    }
}