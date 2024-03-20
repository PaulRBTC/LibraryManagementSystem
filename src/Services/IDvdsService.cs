using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public interface IDvdsService
    {

        Task<Models.Dvd?> GetDvd(long bookId);

        IAsyncEnumerable<Models.Dvd> GetAllDvds();

        Task<bool> UpdateDvd(Models.Dvd newBook);

        Task<bool> DeleteDvd(long bookId);

        Task<Models.Dvd> CreateDvd(Models.Dvd newBook);

        Task<bool> CheckDvdIn(
            long id
        );

        Task<bool> CheckDvdOut(
            long id
        );

    }
}
