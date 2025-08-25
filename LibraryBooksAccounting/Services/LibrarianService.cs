namespace LibraryBooksAccounting;

public class LibrarianService
{
    //TODO: Что сделать:
    //1. Открыть список всех книг
    //1.1. Найти книгу по названию
    //1.2. Найти книгу по автору
    //1.3 Посмотреть историю "аренды" книги
    //2. Открыть список читателей, где будет показана информация о читателях и сколько книг у них на руках
    //2.1. Посмотреть информацию по конкретному читателю с помощью uuid. После чего открывается меню с таблицей всех книг данного читателя
    //2.1.1. Дать книгу читателю. Функция предложит ввести приблизительное название книги или автора. После чего будет записана на читателя
    //2.1.2 Списать книгу с читателя. Читатель вернул книгу
    public void ShowLibrarianMenu()
    {
        while (true)
        {
            Console.Clear();
            LanguageService.PrintLine("LibMenu");
            int result = 0;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                FormattedOutput.Print(LanguageService.GetPhraseByKey("Error-InvalidValue"), $"{ConsoleColor.Red}");
            }

            switch (result)
            {
                case 1:
                    ShowBooksMenu();
                    break;
                case 0:
                    return;
                default:
                    break;
            }
        }
        
    }

    private void ShowBooksMenu()
    {
        
    }
}