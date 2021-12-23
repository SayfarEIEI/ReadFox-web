using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFoxweb
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
