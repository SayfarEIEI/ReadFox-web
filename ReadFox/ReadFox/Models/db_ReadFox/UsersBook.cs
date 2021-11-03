using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFox
{
    public partial class UsersBook
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
    }
}
