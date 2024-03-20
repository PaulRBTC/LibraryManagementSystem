using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public interface IBooksService
    {

        Task<Models.Book>? GetBook(long bookId);

        IAsyncEnumerable<Models.Book> GetAllBooks();

        Task<bool> UpdateBook(Models.Book newBook);

        Task<bool> DeleteBook(long bookId);

        Task<Models.Book> CreateBook(Models.Book newBook);

        Task<bool> CheckBookIn(
            long id
        );

        Task<bool> CheckBookOut(
            long id
        );

    }
}
