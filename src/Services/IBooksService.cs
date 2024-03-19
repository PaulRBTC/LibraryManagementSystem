using System.Collections.Generic;

namespace LibraryManagementSystem.Services
{
    public interface IBooksService
    {

        Models.Book? GetBook(long bookId);

        List<Models.Book> GetAllBooks();

        bool UpdateBook(Models.Book newBook);

        bool DeleteBook(long bookId);

        Models.Book CreateBook(Models.Book newBook);

    }
}
