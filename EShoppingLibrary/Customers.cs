using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EShoppingLibrary
{
    public class Customers
    {
        public string LastError { get; set; }

        public int Add(string Username, string Password, string Email, string Name, string Address, string City, int Zipcode, long PhNum)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            string sql = "Insert INTO Customers values (@Username, @Password, @Email, @Name, @Address, @City, @Zipcode, @PhNum)";

            SqlParameter param1 = new SqlParameter("@Username", SqlDbType.Text);
            param1.Value = Username;
            SqlParameter param2 = new SqlParameter("@Password", SqlDbType.Text);
            param2.Value = Password;
            SqlParameter param3 = new SqlParameter("@Email", SqlDbType.Text);
            param3.Value = Email;
            SqlParameter param4 = new SqlParameter("@Name", SqlDbType.Text);
            param4.Value = Name;
            SqlParameter param5 = new SqlParameter("@Address", SqlDbType.Text);
            param5.Value = Address;
            SqlParameter param6 = new SqlParameter("@City", SqlDbType.Text);
            param6.Value = City;
            SqlParameter param7 = new SqlParameter("@Zipcode", SqlDbType.Decimal);
            param7.Value = Zipcode;
            SqlParameter param8 = new SqlParameter("@PhNum", SqlDbType.Decimal);
            param8.Value = PhNum;

            try
            {
                return aEShoppingConn.ExecuteNonQuery(sql, CommandType.Text, param1, param2, param3, param4, param5, param6, param7, param8);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public int CheckAvailability(string Username)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            
            try
            {
                return aEShoppingConn.getUserNames(Username);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public int ValidateCustomer(string Username, string Password)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();

            try
            {
                return aEShoppingConn.IsValidUser(CommandType.Text, Username, Password);                
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }
    }
}
