using LibraryManagementSystem.Repositories.Base;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Repositories.Impl
{
    public class BooksRepository : BaseRepository<Models.Book>, IBooksRepository
    {

        public BooksRepository(
            IOptions<AppSettings> settings,
            Factories.IBookFactory factory
        ) : base(settings.Value.DatabaseConnectionString, settings.Value.BooksTableName, factory)
        { }

    }
}
