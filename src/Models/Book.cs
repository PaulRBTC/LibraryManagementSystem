﻿using LibraryManagementSystem.Models.Attributes;

namespace LibraryManagementSystem.Models
{
    public class Book : BaseEntity
    {

        [DbColumnName("name")]
        public string Name { get; set; }

        [DbColumnName("author")]
        public string Author { get; set; }

    }
}
