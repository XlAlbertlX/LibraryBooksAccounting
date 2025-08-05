using System.Text.Json;

namespace LibraryBooksAccounting;

public class UserService
{
    private string _DBpath;
    private List<User> _users = new();
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
            return _users;
        }
        
        string json = File.ReadAllText(_DBpath);
        _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        
        
        return _users;
    }

    public bool AddUser(User user)
    {
        if (_users.Any(u => u.Login == user.Login))
        {
            return false;
        }
        user.UUID = Guid.NewGuid().ToString();
        _users.Add(user);
        Save();
        return true;
    }   

    public User GetUserByLogin(string login)
    {
        return _users.FirstOrDefault(u => u.Login == login.ToLower());
    }
    
    public bool UserExists(string login)
    {
        return _users.Any(u => u.Login == login.ToLower());
    }

    private void Save()
    {
        string json = JsonSerializer.Serialize(_users);
        File.WriteAllText(_DBpath, json);
    }
}