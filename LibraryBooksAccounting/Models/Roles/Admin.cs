namespace LibraryBooksAccounting;

public class Admin : User
{
    public override Roles Role => Roles.Admin;
    public string AdminName { get; set; }
}