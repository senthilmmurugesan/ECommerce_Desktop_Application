using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingApplication
{
    class Member
    {
        private string username;
        private string password;
        private string email;
        private string name;

        public Member()
        {
            //Default Constructor
        }

        public Member(string username, string password, string email, string name)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (!value.All(Char.IsLetterOrDigit))
                    throw new Exception("Username should not contain special characters and spaces.");
                else if (value.Length < 5)
                    throw new Exception("Username should be atleast 5 characters..");
                else
                    username = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length < 6)
                    throw new Exception("Password should be atleast 6 characters..");
                else
                    password = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!(value.Split('@').Length == 2 & value.Split('.').Length == 2))
                    throw new Exception("Invalid Email.");
                else
                    email = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!value.All(c => Char.IsLetter(c) || c == ' '))
                    throw new Exception("Name should contain only alphabets.");
                else
                    name = value;
            }
        }
    }
}
