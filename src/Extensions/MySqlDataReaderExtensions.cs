using MySql.Data.MySqlClient;

namespace LibraryManagementSystem.Extensions
{
    public static class MySqlDataReaderExtensions
    {

        public static bool IsDBNull(
            this MySqlDataReader @this,
            string columnName
        )
        {
            int colOrd = @this.GetOrdinal(columnName);
            return @this.IsDBNull(colOrd);
        }

    }
}
