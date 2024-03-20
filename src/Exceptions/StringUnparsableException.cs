using System;

namespace LibraryManagementSystem.Exceptions
{
    public class StringUnparsableException : Exception
    {

        public StringUnparsableException(string type)
            : base($"String could not be parsed to {type}.")
        { }

    }
}
