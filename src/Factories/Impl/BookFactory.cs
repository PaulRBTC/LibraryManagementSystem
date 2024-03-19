using LibraryManagementSystem.Extensions;
using MySql.Data.MySqlClient;

namespace LibraryManagementSystem.Factories.Impl
{
    public class BookFactory : IBookFactory
    {

        public Models.Book BuildEntityFromReader(
            MySqlDataReader reader
        )
        {
            return new Models.Book
            {
                Id = reader.GetInt64("id"),
                Name = reader.GetString("name"),
                Author = reader.GetString("author"),
                CheckedInAt = reader.GetDateTime("checked_in_at"),
                CheckedOutAt = reader.IsDBNull("checked_out_at") ? null : reader.GetDateTime("checked_out_at"),
                CreatedAt = reader.GetDateTime("created_at"),
                LastUpdatedAt = reader.GetDateTime("last_updated_at"),
            };
        }

    }
}
