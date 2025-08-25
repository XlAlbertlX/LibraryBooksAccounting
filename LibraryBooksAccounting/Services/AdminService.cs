using System.Diagnostics;

namespace LibraryBooksAccounting;
using ConsoleTables;
public class AdminService
{
   
    private UserService _userService = new ("../../.././DB/Users.json");
    public void ShowAdminMenu()
    {
        while (true)
        {
            Console.Clear();
            LanguageService.PrintLine("AdminMenu");
            int result = 0;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-InvalidValue"), $"{ConsoleColor.Red}");
            }

            switch (result)
            {
                case 1:
                    ShowUsersTable();
                    
                    break;
                case 0:
                    return;
                default:
                    break;
            }
        }
        
    }

    private void ShowUsersTable()
    {
        bool ShowMenu = true;
        while (ShowMenu)
        {
            Console.Clear();
            var usersList = _userService.GetAllUsers();
            var table = new ConsoleTable("UUID", "Username", "Name", "Surname", "Role");
            foreach (var user in usersList)
            {
                table.AddRow(user.UUID, user.Login, user.Name, user.Surname, user.Role);
            }
            Format format = Format.Minimal;
            table.Write(format);
        
            LanguageService.PrintLine("AdminPanel-ChooseActionWithUsers");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                {
                    _userService.CreateUser();
                    break;
                }
                case ConsoleKey.D2:
                {
                    LanguageService.PrintLine("UUIDInput");
                    string UUID = Console.ReadLine();
                    _userService.DeleteUser(UUID);
                    break; 
                }
                case ConsoleKey.D3:
                {
                    LanguageService.PrintLine("UUIDInput");
                    string UUID = Console.ReadLine();
                    if (string.IsNullOrEmpty(UUID))
                    {
                        FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-InvalidValue"), $"{ConsoleColor.Red}");
                        break;
                    }
                    SeeInfoAboutUser(UUID);
                    break; 
                }
                case ConsoleKey.D0:
                {
                    ShowMenu = false;
                    break;
                }
            } 
        }
        
    }

    private void SeeInfoAboutUser(string UUID)
    {
        var user = _userService.GetUserByUUID(UUID);
        if (user is null)
        {
            FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-InvalidValue"), $"{ConsoleColor.Red}");
            return;
        }
        switch (user.Role)
        {
            case Roles.Admin:
            {
                Console.Clear();
                var table = new ConsoleTable("UUID", "Login", "Name", "Surname", "Role");
                table.AddRow(user.UUID, user.Login, user.Name, user.Surname, user.Role);
                Format format = Format.Minimal;
                table.Write(format);
                
                LanguageService.PrintLine("AdminPanel-ActionsWithUser");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        _userService.EditUser((Admin)user);
                        break;
                }
                break;
            }
            case Roles.Reader:
            {
                Console.Clear();
                Reader reader = (Reader)user;
                var table = new ConsoleTable("UUID", "Login", "Name", "Surname", "Role", "BooksCount");
                table.AddRow(reader.UUID, reader.Login, reader.Name, reader.Surname, reader.Role, reader.BooksCount);
                Format format = Format.Minimal;
                table.Write(format);
                LanguageService.PrintLine("AdminPanel-ActionsWithUser");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        _userService.EditUser((Reader)user);
                        break;
                }
                break;
            }
            case Roles.Librarian:
                break;
        }
        
    }
    
}