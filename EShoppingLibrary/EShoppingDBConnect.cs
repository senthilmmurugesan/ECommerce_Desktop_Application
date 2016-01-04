using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EShoppingLibrary
{
    class EShoppingDBConnect
    {
        private SqlConnection connection;

        string conString = ConfigurationManager.ConnectionStrings["EShoppingConnection"].ToString();
        //string conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Senthil Kumar\Documents\Visual Studio 2012\Projects\EShoppingApplication\EShoppingLibrary\Database.mdf;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            if (connection == null)
                connection = new SqlConnection(conString);
            return connection;
        }

        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void Close()
        {
            connection.Close();
        }

        public SqlDataReader GetReader(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null,
                                SqlParameter param5 = null, SqlParameter param6 = null, SqlParameter param7 = null, SqlParameter param8 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;


            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);
            if (param5 != null)
                cmd.Parameters.Add(param5);
            if (param6 != null)
                cmd.Parameters.Add(param6);
            if (param7 != null)
                cmd.Parameters.Add(param7);
            if (param8 != null)
                cmd.Parameters.Add(param8);

            try
            {
                this.Open();
                return cmd.ExecuteReader();
            }
            catch
            {
                this.Close();
                return null;
            }

        }

        public int IsValidUser(CommandType cmdType, string param1, string param2)
        {

            string procnameOrQuery = String.Format("select Count(*) from Customers where Username = '{0}' and Password = '{1}'",
                                        param1, param2);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(procnameOrQuery, this.GetConnection());
            this.Open();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
                return 1;
            else
                return 0;
        }

        public int getUserNames(string username)
        {
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Customers where Username=@Name", conn);
            
            cmd.Parameters.AddWithValue("@Name", username);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
                return 1;
            else
                return 0;
        }

        public DataTable GetTable(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null)
        {

            //Load a DataTable form the database using either query text or a stored procedure name.
            DataTable table = new DataTable();
            SqlDataReader reader = null;

            try
            {
                reader = GetReader(procnameOrQuery, cmdType, param1, param2, param3, param4);
                table.Load(reader);
                reader.Close();
                return table;

            }
            finally
            {
                this.Close();
            }

        }
        public object GetScalar(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;

            //If there are query parameters, add them to the command

            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);

            try
            {
                this.Open();
                return cmd.ExecuteScalar();

            }
            finally
            {
                this.Close();
                cmd.Dispose();
            }
        }

        public int ExecuteNonQuery(string procnameOrQuery, CommandType cmdType, SqlParameter param1 = null, SqlParameter param2 = null, SqlParameter param3 = null, SqlParameter param4 = null,
                                    SqlParameter param5 = null, SqlParameter param6 = null, SqlParameter param7 = null, SqlParameter param8 = null)
        {

            SqlCommand cmd = new SqlCommand(procnameOrQuery, this.GetConnection());
            cmd.CommandType = cmdType;
            
            //If there are query parameters, add them to the command

            if (param1 != null)
                cmd.Parameters.Add(param1);
            if (param2 != null)
                cmd.Parameters.Add(param2);
            if (param3 != null)
                cmd.Parameters.Add(param3);
            if (param4 != null)
                cmd.Parameters.Add(param4);
            if (param5 != null)
                cmd.Parameters.Add(param5);
            if (param6 != null)
                cmd.Parameters.Add(param6);
            if (param7 != null)
                cmd.Parameters.Add(param7);
            if (param8 != null)
                cmd.Parameters.Add(param8);

            try
            {
                this.Open();
                return cmd.ExecuteNonQuery();

            }
            finally
            {
                this.Close();
                cmd.Dispose();
            }
        }
        public DataSet GetDataSet(string sql)
        {
            DataSet aDataset = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, this.GetConnection());
                this.Open();
                adapter.Fill(aDataset);
                return aDataset;
            }
            finally
            {
                this.Close();
            }
        }

        public int ExecuteNonQuery1(string ItemName, int noOfItems)
        {
            string sql = String.Format("update Products set Stock = Stock - {0} where ItemName = '{1}'", noOfItems, ItemName);
            SqlCommand cmd = new SqlCommand(sql, this.GetConnection());
            try
            {
                this.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                this.Close();
                cmd.Dispose();
            }
        }

        public bool CheckStockAvailability(string name, int items)
        {
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            //string sql = String.Format("select '{0}' from Products where Stock - {1} < 0", name, items);
            string sql = String.Format("select * from Products where ItemName = '{0}' and Stock - {1} < 0", name, items);
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
                return true;
            else
                return false;
        }
    }
}
