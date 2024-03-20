using LibraryManagementSystem.Repositories.Base;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Repositories.Impl
{
    public class DvdsRepository : BaseRepository<Models.Dvd>, IDvdsRepository
    {

        public DvdsRepository(
            IOptions<AppSettings> settings,
            Factories.IDvdFactory factory
        ) : base(settings.Value.DatabaseConnectionString, settings.Value.DvdsTableName, factory)
        { }

    }
}
