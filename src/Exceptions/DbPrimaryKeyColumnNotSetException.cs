using System;

namespace LibraryManagementSystem.Exceptions
{
    public class DbPrimaryKeyColumnNotSetException : Exception
    {

        public DbPrimaryKeyColumnNotSetException(string modelName)
            : base($"Model {modelName} does not have any properties with the 'DbIsPrimaryKey' attribute set.")
        {

        }

    }
}
