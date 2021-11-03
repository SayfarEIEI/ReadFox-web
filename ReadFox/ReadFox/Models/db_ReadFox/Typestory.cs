using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFox
{
    public partial class Typestory
    {
        public Typestory()
        {
            Books = new HashSet<Book>();
        }

        public int TypestoryId { get; set; }
        public string TypestoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
