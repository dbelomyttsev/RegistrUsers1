using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrUsers1
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; } 
    }
}
