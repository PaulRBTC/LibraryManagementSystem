using LibraryManagementSystem.Models.Attributes;
using System;

namespace LibraryManagementSystem.Models
{
    public class Book : BaseEntity
    {

        [DbColumnName("name")]
        public string Name { get; set; }

        [DbColumnName("author")]
        public string Author { get; set; }

        [DbColumnName("checked_in_at")]
        public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;

        [DbColumnName("checked_out_at")]
        public DateTime? CheckedOutAt { get; set; } = null;

        [DbIgnore]
        public bool IsCheckedIn => CheckedOutAt is null && CheckedInAt <= DateTime.UtcNow;

        public override string ToString()
        {
            return $"{Number} - {Name}";
        }

    }
}
