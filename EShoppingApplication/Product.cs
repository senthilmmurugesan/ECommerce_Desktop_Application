using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingApplication
{
    class Product : IProduct
    {
        public int ProdID { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int Stock { get; set; }
        public byte[] Image { get; set; }
        public string Category { get; set; }

        public static List<Product> allProducts = new List<Product>();

        public Product()
        {
            //default constructor
        }

        public Product(int prodId, string name, double price, int stock, byte[] image, string category)
        {
            ProdID = prodId;
            ItemName = name;
            ItemPrice = price;
            Stock = stock;
            Image = image;
            Category = category;
        }

    }
}
