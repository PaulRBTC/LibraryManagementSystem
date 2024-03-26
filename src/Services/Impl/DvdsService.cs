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

        public Task<bool> CheckDvdIn(
            long id
        ) => _repo.CheckIn(id);

        public Task<bool> CheckDvdOut(
            long id
        ) => _repo.CheckOut(id);

    }
}
