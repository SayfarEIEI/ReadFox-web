using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFox
{
    public partial class Book
    {
        public string ProductName { get; set; }
        public string Author { get; set; }
        public int? TypestoryId { get; set; }
        public string ImageName { get; set; }
        public int? Price { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Typestory Typestory { get; set; }
    }
}
