using MySql.Data.MySqlClient;

namespace LibraryManagementSystem.Factories.Base
{
    public interface IEntityFactory<T> where T : class, new()
    {

        T BuildEntityFromReader(MySqlDataReader reader);

    }
}
