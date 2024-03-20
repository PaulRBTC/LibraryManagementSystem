using System.Data.Common;

namespace LibraryManagementSystem.Factories.Base
{
    public interface IEntityFactory<T> where T : class, new()
    {

        T BuildEntityFromReader(DbDataReader reader);

    }
}
