using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingApplication
{
    class Administrator : Member
    {
        public Administrator(string username, string password, string email, string name)
            : base(username, password, email, name)
        {
            //Calls Base constructor
        }
    }
}
