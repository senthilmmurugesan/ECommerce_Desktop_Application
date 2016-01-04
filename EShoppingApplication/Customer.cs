using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingApplication
{
    class Customer : Member
    {
        public string Address { get; set; }
        public string City { get; set; }

        private int zipcode;
        private long phNum;

        public Customer(string username, string password, string email, string name, string address, string city, int zipcode, long phNum)
            : base(username, password, email, name)
        {
            Address = address;
            City = city;
            Zipcode = zipcode;
            PhNum = phNum;
        }

        public int Zipcode
        {
            get { return zipcode; }
            set
            {
                if (value.ToString().Length != 5)
                    throw new Exception("Invalid Zipcode!");
                else
                    zipcode = value;
            }
        }

        public long PhNum
        {
            get { return phNum; }
            set
            {
                if (value.ToString().Length != 10)
                    throw new Exception("Invalid Phone Number!");
                else
                    phNum = value;
            }
        }
    }
}
