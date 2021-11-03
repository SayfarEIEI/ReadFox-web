using System;
using System.Collections.Generic;

#nullable disable

namespace ReadFox.Models.db_ReadFox
{
    public partial class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
