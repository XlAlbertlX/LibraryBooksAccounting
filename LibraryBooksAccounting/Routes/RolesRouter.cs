namespace LibraryBooksAccounting.Routes;

public class RolesRouter
{
    private static AdminService _adminService = new();
    public static void Route(Roles role)
    {
        switch (role)
        {
            case Roles.Admin:
                _adminService.ShowAdminMenu();
                break;
            case Roles.Librarian:
                Console.WriteLine("Librarian menu");
                break;
            case Roles.Reader:
                Console.WriteLine("Reader menu");
                break;
        }
    } 
}