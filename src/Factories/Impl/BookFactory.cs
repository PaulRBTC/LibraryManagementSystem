﻿using LibraryManagementSystem.Extensions;
using System.Data.Common;

namespace LibraryManagementSystem.Factories.Impl
{
    public class BookFactory : IBookFactory
    {

        public Models.Book BuildEntityFromReader(
            DbDataReader reader
        )
        {
            return new Models.Book
            {
                Id = reader.GetColumnValue("id").ToLong(),
                Name = reader.GetColumnValue("name"),
                Author = reader.GetColumnValue("author"),
                CheckedInAt = reader.GetColumnValue("checked_in_at").ToDateTime(),
                CheckedOutAt = reader.GetColumnValue("checked_out_at").ToNullableDateTime(),
                CreatedAt = reader.GetColumnValue("created_at").ToDateTime(),
                LastUpdatedAt = reader.GetColumnValue("last_updated_at").ToDateTime(),
            };
        }

    }
}
