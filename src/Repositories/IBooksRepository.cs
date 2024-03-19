using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IBooksRepository
    {

        Models.Book? Get(long id);

        List<Models.Book> GetAll();

        bool Update(Models.Book @new);

        Models.Book Create(Models.Book @new);

        bool Delete(long id);

    }
}
