using LibraryManagementSystem.Models.Attributes;
using System;

namespace LibraryManagementSystem.Models
{
    public class BaseEntity
    {

        [DbIsPrimaryKey]
        [DbColumnName("id")]
        public long? Id { get; set; } = null;

        [DbColumnName("checked_in_at")]
        public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;

        [DbColumnName("checked_out_at")]
        public DateTime? CheckedOutAt { get; set; } = null;

        [DbIgnore]
        public bool IsCheckedIn => CheckedOutAt is null && CheckedInAt <= DateTime.UtcNow;

        [DbColumnName("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DbColumnName("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
