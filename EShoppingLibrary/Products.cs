using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EShoppingLibrary
{
    public class Products
    {
        public string LastError { get; set; }

        public int Add(int ProdID, string ItemName, double ItemPrice, int Stock, byte[] Image, string Category)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            string sql = "Insert INTO Products values (@ProdId, @ItemName, @ItemPrice, @Stock, @Image, @Category)";

            SqlParameter param1 = new SqlParameter("@ProdId", SqlDbType.Int);
            param1.Value = ProdID;
            SqlParameter param2 = new SqlParameter("@ItemName", SqlDbType.Text);
            param2.Value = ItemName;
            SqlParameter param3 = new SqlParameter("@ItemPrice", SqlDbType.Decimal);
            param3.Value = ItemPrice;
            SqlParameter param4 = new SqlParameter("@Stock", SqlDbType.Int);
            param4.Value = Stock;
            SqlParameter param5 = new SqlParameter("@Image", SqlDbType.Image);
            param5.Value = Image;
            SqlParameter param6 = new SqlParameter("@Category", SqlDbType.Text);
            param6.Value = Category;

            try
            {
                return aEShoppingConn.ExecuteNonQuery(sql, CommandType.Text, param1, param2, param3, param4, param5, param6);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }
        
        public Image LoadImage(int prodID)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            string query = String.Format("select Image from Products where ProdID = {0}", prodID);
            Image image = null;
            SqlDataReader dr = aEShoppingConn.GetReader(query, CommandType.Text);

            while (dr.Read())
            {
                byte[] arr = (byte[])(dr["image"]);
                image = byteArrayToImage(arr);
            }
            return image;
        }

        public string LoadLabel(int prodID)
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            string query = String.Format("select * from Products where ProdID = {0}", prodID);
            string name = "";
            double price = 0;
            int stock = 0; 
            SqlDataReader dr = aEShoppingConn.GetReader(query, CommandType.Text);

            while (dr.Read())
            {
                name = dr.GetString(dr.GetOrdinal("ItemName"));
                price = (double)dr.GetDecimal(dr.GetOrdinal("ItemPrice"));
                stock = (int)dr.GetInt32(dr.GetOrdinal("Stock"));
            }
            return (name.Trim() + "  $" + price + "  " + stock.ToString() + " Available");
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }

        public int[] LoadProdIDs()
        {
            EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
            int[] allProdIDs = new int[50]; int i = 0;
            SqlDataReader dr = aEShoppingConn.GetReader("select ProdID from Products", CommandType.Text);

            while (dr.Read())
            {
                allProdIDs[i++] = (int)(dr["ProdID"]);
            }
            return allProdIDs;
        }

        public string GetData(int prodId)
        {
            try
            {
                EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
                string query = "select * from Products where ProdID = @ID";

                SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                param1.Value = prodId;
                string res = "";
                SqlDataReader dr = aEShoppingConn.GetReader(query, CommandType.Text, param1);

                while (dr.Read())
                {
                    string name = dr.GetString(dr.GetOrdinal("ItemName"));
                    string category = dr.GetString(dr.GetOrdinal("Category"));
                    double price = (double)dr.GetDecimal(dr.GetOrdinal("ItemPrice"));
                    int stock = (int)dr.GetInt32(dr.GetOrdinal("Stock"));
                    res = name + "%" + category + "%" + price + "%" + stock;
                }
                return res;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public int UpdateProduct(double newPrice, int newStock, int prodID)
        {
            try
            {
                EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
                string query = "update Products set Stock = @newStock, ItemPrice = @newPrice " +
                    "where ProdID = @ID";

                SqlParameter param1 = new SqlParameter("@ID", SqlDbType.Int);
                param1.Value = prodID;
                SqlParameter param2 = new SqlParameter("@newPrice", SqlDbType.Decimal);
                param2.Value = newPrice;
                SqlParameter param3 = new SqlParameter("@newStock", SqlDbType.Int);
                param3.Value = newStock;

                return aEShoppingConn.ExecuteNonQuery(query, CommandType.Text, param1, param2, param3);

            }

            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public int DecrementStock(string ItemName, int noOfItems)
        {
            try
            {
                EShoppingDBConnect aEShoppingConn = new EShoppingDBConnect();
                
                if (aEShoppingConn.CheckStockAvailability(ItemName, noOfItems))
                    throw new Exception("One of the Items you have ordered exceeds the stock available right now. Please check the quantity.");
                return aEShoppingConn.ExecuteNonQuery1(ItemName, noOfItems);
            }

            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }
    }
}
