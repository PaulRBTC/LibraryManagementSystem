using System;

namespace LibraryManagementSystem.Models
{
    public class ListViewModel
    {

        public int Id { get; set; }

        public string Number => $"#{Id + 1}";

        public string Title { get; set; }

        public string Owner { get; set; }

        public bool IsCheckedIn { get; set; }

        public Type Represents { get; set; }

        public long RepresentsId { get; set; }

        public override string ToString()
        {
            return $"{Number} - {Title}";
        }

    }
}
