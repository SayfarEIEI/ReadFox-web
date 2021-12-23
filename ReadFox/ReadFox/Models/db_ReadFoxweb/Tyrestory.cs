using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFoxweb
{
    public partial class Tyrestory
    {
        public Tyrestory()
        {
            Books = new HashSet<Book>();
        }

        public int TypestoryId { get; set; }
        public string TypestoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
