namespace LibraryBooksAccounting;

public class Employee : User
{
    public string email { get; set; }
    public string position { get; set; }
    public string address { get; set; }
    public string phoneNumber { get; set; }
    public DateTime dateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public bool status { get; set; }
    public Schedule workSchedule { get; set; }
}