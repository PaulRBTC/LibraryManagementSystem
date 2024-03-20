using LibraryManagementSystem.Models.Attributes;

namespace LibraryManagementSystem.Models
{
    public class Dvd : BaseEntity
    {

        [DbColumnName("name")]
        public string Name { get; set; }

        [DbColumnName("director")]
        public string Director { get; set; }

    }
}
