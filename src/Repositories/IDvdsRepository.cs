using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public interface IDvdsRepository
    {

        Task<Models.Dvd?> Get(long id);

        IAsyncEnumerable<Models.Dvd> GetAll();

        Task<bool> Update(Models.Dvd @new);

        Task<Models.Dvd> Create(Models.Dvd @new);

        Task<bool> Delete(long id);

    }
}
