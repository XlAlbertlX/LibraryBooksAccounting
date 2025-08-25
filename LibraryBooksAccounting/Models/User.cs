using System.Text.Json.Serialization;

namespace LibraryBooksAccounting;


public abstract class User
{
    public string? UUID { get; set; }
    public string? Login {get; set;}
    public string? Password {get; set;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public abstract Roles Role {get;}
}