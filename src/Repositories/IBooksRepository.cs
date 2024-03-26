using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public interface IBooksRepository
    {

        Task<Models.Book>? Get(long id);

        IAsyncEnumerable<Models.Book> GetAll();

        Task<bool> Update(Models.Book @new);

        Task<Models.Book> Create(Models.Book @new);

        Task<bool> Delete(long id);

        Task<bool> CheckIn(long id);

        Task<bool> CheckOut(long id);

    }
}
