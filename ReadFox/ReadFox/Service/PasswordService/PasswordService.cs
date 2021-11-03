using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReadFox.Service.PasswordService
{
    public class PasswordService
    {
        public string Password(string password) 
        { 
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool CheckPassword(string password,string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password,hashPassword) ;
        }
    }
}
