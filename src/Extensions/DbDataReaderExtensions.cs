using System.Data.Common;

namespace LibraryManagementSystem.Extensions
{
    public static class DbDataReaderExtensions
    {

        public static bool IsDBNull(
            this DbDataReader @this,
            string columnName
        )
        {
            int colOrd = @this.GetOrdinal(columnName);
            return @this.IsDBNull(colOrd);
        }

        public static string? GetColumnValue(
            this DbDataReader @this,
            string columnName
        )
        {
            int colOrd = @this.GetOrdinal(columnName);

            if (@this.IsDBNull(colOrd))
            {
                return null;
            }

            return @this[colOrd].ToString();
        }

    }
}
