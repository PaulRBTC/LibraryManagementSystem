using System;

namespace LibraryManagementSystem.Exceptions
{
    public class DbColumnDefinedMoreThanOnceException : Exception
    {

        public DbColumnDefinedMoreThanOnceException(string modelName, string columnName)
            : base($"Model {modelName} has column '{columnName}' defined more than once.")
        { }

    }
}
