using System;

namespace LibraryManagementSystem.Exceptions
{
    public class DbPrimaryKeyDefinedMoreThanOnceException : Exception
    {

        public DbPrimaryKeyDefinedMoreThanOnceException(string modelName)
            : base($"Model {modelName} has more than one primary key defined.")
        { }

    }
}
