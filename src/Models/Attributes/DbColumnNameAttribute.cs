using System;

namespace LibraryManagementSystem.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DbColumnNameAttribute : Attribute
    {

        public string ColumnName { get; private set; }

        public DbColumnNameAttribute(
            string columnName
        )
        {
            ColumnName = columnName;
        }

    }
}
