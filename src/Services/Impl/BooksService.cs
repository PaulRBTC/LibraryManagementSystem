using System.Collections.Generic;

namespace LibraryManagementSystem.Services.Impl
{
    public class BooksService : IBooksService
    {

        private readonly Repositories.IBooksRepository _repo;

        public BooksService(
            Repositories.IBooksRepository repo
        )
        {
            _repo = repo;
        }

        public Models.Book CreateBook(
            Models.Book newBook
        ) => _repo.Create(newBook);

        public bool DeleteBook(
            long bookId
        ) => _repo.Delete(bookId);

        public List<Models.Book> GetAllBooks() => _repo.GetAll();

        public Models.Book? GetBook(
            long bookId
        ) => _repo.Get(bookId);

        public bool UpdateBook(
            Models.Book newBook
        ) => _repo.Update(newBook);

    }
}
