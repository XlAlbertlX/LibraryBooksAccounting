namespace LibraryBooksAccounting;

public class SERVER_CONFIG
{
    private string adress = "localhost";
    private string username = "postgres";
    private string password = "854375";
    private string database = "library";
    
    public string GetConfig()
    {
        return $"Host={adress};Port=5432;Username={username};Password={password};Database={database}";
    }
}