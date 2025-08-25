namespace LibraryBooksAccounting;

public class Admin : User
{
    public override Roles Role => Roles.Admin;
}