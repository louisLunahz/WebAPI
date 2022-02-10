using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoppingPortal
{
    public class LoginCredentials
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginCredentials(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public LoginCredentials()
        {
        }
    }
}
