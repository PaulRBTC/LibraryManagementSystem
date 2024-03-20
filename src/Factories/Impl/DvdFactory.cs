using LibraryManagementSystem.Extensions;
using System.Data.Common;

namespace LibraryManagementSystem.Factories.Impl
{
    public class DvdFactory : IDvdFactory
    {

        public Models.Dvd BuildEntityFromReader(
            DbDataReader reader
        )
        {
            return new Models.Dvd
            {
                Id = reader.GetColumnValue("id").ToLong(),
                Name = reader.GetColumnValue("name"),
                Director = reader.GetColumnValue("director"),
                CheckedInAt = reader.GetColumnValue("checked_in_at").ToDateTime(),
                CheckedOutAt = reader.GetColumnValue("checked_out_at").ToNullableDateTime(),
                CreatedAt = reader.GetColumnValue("created_at").ToDateTime(),
                LastUpdatedAt = reader.GetColumnValue("last_updated_at").ToDateTime(),
            };
        }

    }
}
