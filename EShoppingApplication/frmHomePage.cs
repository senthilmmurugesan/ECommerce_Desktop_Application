using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EShoppingApplication
{
    public partial class frmHomePage : Form
    {
        public frmHomePage()
        {
            InitializeComponent();
        }

        public void frmHomePage_Load(object sender, EventArgs e)
        {
            EShoppingLibrary.Products aProduct = new EShoppingLibrary.Products();

            int[] allIDs = aProduct.LoadProdIDs();

            pictureBox1.Image = aProduct.LoadImage(allIDs[0]);
            pictureBox2.Image = aProduct.LoadImage(allIDs[1]);
            pictureBox3.Image = aProduct.LoadImage(allIDs[2]);
            pictureBox4.Image = aProduct.LoadImage(allIDs[3]);
            pictureBox5.Image = aProduct.LoadImage(allIDs[4]);
            pictureBox6.Image = aProduct.LoadImage(allIDs[5]);

            linkLabel1.Text = aProduct.LoadLabel(allIDs[0]);
            linkLabel2.Text = aProduct.LoadLabel(allIDs[1]);
            linkLabel3.Text = aProduct.LoadLabel(allIDs[2]);
            linkLabel4.Text = aProduct.LoadLabel(allIDs[3]);
            linkLabel5.Text = aProduct.LoadLabel(allIDs[4]);
            linkLabel6.Text = aProduct.LoadLabel(allIDs[5]);
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);
            return img;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel1.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel2.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel3.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel4.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel5.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Product aProd = new Product();

            string label = linkLabel6.Text;
            string[] val = label.Split(new string[] { "  " }, StringSplitOptions.None);

            string name = val[0];
            double price = Convert.ToDouble(val[1].Substring(1));
            int stock = Convert.ToInt32(val[2].Split(' ')[0]);

            aProd.ItemName = name;
            aProd.ItemPrice = price;
            aProd.Stock = stock;

            if (aProd.Stock == 0)
                MessageBox.Show("Item not available right now...");
            else
            {
                Product.allProducts.Add(aProd);
                MessageBox.Show("Item added to cart...");
            }
        }
    }
}
