using System;

namespace LibraryManagementSystem.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DbIsPrimaryKeyAttribute : Attribute
    {
    }
}
