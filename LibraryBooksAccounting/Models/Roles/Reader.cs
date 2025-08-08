namespace LibraryBooksAccounting;

public class Reader : User
{
    public override Roles Role => Roles.Reader;
    public int BooksCount { get; set; }

    public Reader(string login, string name, string surname, string password)
    {
        Login = login;
        Name = name;
        Surname = surname;
        Password = password;
    }
}