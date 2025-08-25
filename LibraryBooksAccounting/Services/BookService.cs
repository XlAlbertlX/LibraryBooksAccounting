using Newtonsoft.Json;

namespace LibraryBooksAccounting;

public class BookService
{
    private List<Book> _booksList;

    public BookService(string booksFilePath = "")
    {
        string jsonString = File.ReadAllText(booksFilePath);
        _booksList = JsonConvert.DeserializeObject<List<Book>>(jsonString) ?? new List<Book>();
        
    }
    
    public List<Book> GetBooksList()
    {
        return _booksList; 
    }

    public void AddBook(Book book)
    {
        _booksList.Add(book);
    }

    private void RemoveBook(Book book)
    {
        _booksList.Remove(book);
    }
}