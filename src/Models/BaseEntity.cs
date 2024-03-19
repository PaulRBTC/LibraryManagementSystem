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

        [DbColumnName("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DbColumnName("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
