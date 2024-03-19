using LibraryManagementSystem.Models.Attributes;
using System;

namespace LibraryManagementSystem.Models
{
    public class BaseEntity
    {

        [DbIsPrimaryKey]
        [DbColumnName("id")]
        public long? Id { get; set; } = null;

        [DbIgnore]
        public string Number => $"#{Id}";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
