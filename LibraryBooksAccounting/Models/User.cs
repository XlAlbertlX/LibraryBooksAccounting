namespace LibraryBooksAccounting;

public class User
{
    public string? UUID { get; set; }
    public string? Login {get; set;}
    public string? Password {get; set;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public Roles Role {get; set;}
}