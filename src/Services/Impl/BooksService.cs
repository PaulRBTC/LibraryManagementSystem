﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<Models.Book> CreateBook(
            Models.Book newBook
        ) => _repo.Create(newBook);

        public Task<bool> DeleteBook(
            long bookId
        ) => _repo.Delete(bookId);

        public IAsyncEnumerable<Models.Book> GetAllBooks() => _repo.GetAll();

        public Task<Models.Book>? GetBook(
            long bookId
        ) => _repo.Get(bookId);

        public Task<bool> UpdateBook(
            Models.Book newBook
        )
        {
            newBook.LastUpdatedAt = DateTime.UtcNow;
            return _repo.Update(newBook);
        }

        public Task<bool> CheckBookIn(
            long id
        ) => _repo.CheckIn(id);

        public Task<bool> CheckBookOut(
            long id
        ) => _repo.CheckOut(id);

    }
}
