namespace LibraryBooksAccounting;

public class Schedule
{
    public Dictionary<DayOfWeek, string> Days{ get; set; }

    public Schedule()
    {
        Days = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Sunday, "Выходной" },
            { DayOfWeek.Monday, "09:00-17:00" },
            { DayOfWeek.Tuesday, "09:00-17:00" },
            { DayOfWeek.Wednesday, "09:00-17:00" },
            { DayOfWeek.Thursday, "09:00-17:00" },
            { DayOfWeek.Friday, "09:00-17:00" },
            { DayOfWeek.Saturday, "10:00-15:00" },
        };
    }

    public void SetDay(DayOfWeek dayOfWeek, string hours)
    {
        Days[dayOfWeek] = hours;
    }

    public string GetDay(DayOfWeek dayOfWeek)
    {
        return Days.ContainsKey(dayOfWeek) ? Days[dayOfWeek] : "Нет данных";
    }
}