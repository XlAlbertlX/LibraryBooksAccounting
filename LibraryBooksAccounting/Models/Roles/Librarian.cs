namespace LibraryBooksAccounting;

public class Librarian : User
{
    public override Roles Role => Roles.Librarian;
    public string email { get; set; }
    public string address { get; set; }
    public string phoneNumber { get; set; }
    public DateTime dateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public bool status { get; set; }
    public Schedule workSchedule { get; set; }
}