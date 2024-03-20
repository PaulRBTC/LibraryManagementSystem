using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Impl
{
    public class DvdsService : IDvdsService
    {

        private readonly Repositories.IDvdsRepository _repo;

        public DvdsService(
            Repositories.IDvdsRepository repo
        )
        {
            _repo = repo;
        }

        public Task<Models.Dvd> CreateDvd(
            Models.Dvd newDvd
        ) => _repo.Create(newDvd);

        public Task<bool> DeleteDvd(
            long bookId
        ) => _repo.Delete(bookId);

        public IAsyncEnumerable<Models.Dvd> GetAllDvds() => _repo.GetAll();

        public Task<Models.Dvd?> GetDvd(
            long bookId
        ) => _repo.Get(bookId);

        public Task<bool> UpdateDvd(
            Models.Dvd newDvd
        )
        {
            newDvd.LastUpdatedAt = DateTime.UtcNow;
            return _repo.Update(newDvd);
        }

        public async Task<bool> CheckDvdIn(
            long id
        )
        {
            var book = await GetDvd(id);
            if (book != null)
            {
                book.CheckedInAt = DateTime.UtcNow;
                book.CheckedOutAt = null;
                return await UpdateDvd(book);
            }

            return false;
        }

        public async Task<bool> CheckDvdOut(
            long id
        )
        {
            var book = await GetDvd(id);
            if (book != null)
            {
                book.CheckedOutAt = DateTime.UtcNow;
                return await UpdateDvd(book);
            }

            return false;
        }

    }
}
