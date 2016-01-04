using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EShoppingApplication
{
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()  
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string picloc = "";
            if (dlg.ShowDialog() == DialogResult.OK)
                picloc = dlg.FileName.ToString();
            imageTextBox.Text = picloc;
            imageTextBox.Enabled = false;
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                EShoppingLibrary.Products aProd = new EShoppingLibrary.Products();

                int prodId = Convert.ToInt32(productIdTextBox.Text);
                string productName = productNameTextBox.Text;
                string category = categoryTextBox.Text;
                double itemPrice = Convert.ToDouble(itemPriceTextBox.Text);
                int noOfItems = Convert.ToInt32(noOfItemsTextBox.Text);

                Image img = Image.FromFile(imageTextBox.Text);

                byte[] data;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    data = ms.ToArray();
                }

                Product aProduct = new Product(prodId, productName, itemPrice, noOfItems, data, category);

                //Adding to Database
                int res = aProd.Add(prodId, productName, itemPrice, noOfItems, data, category);
                if (res == 1)
                    MessageBox.Show("Added to the database!");
                else
                    MessageBox.Show(aProd.LastError);
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Please enter all the fields..", "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }

        private void viewProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (productIdTextBox.Text.Trim() != "")
                {
                    int prodId = Convert.ToInt32(productIdTextBox.Text);
                    EShoppingLibrary.Products aProd = new EShoppingLibrary.Products();

                    string res = aProd.GetData(prodId);
                    if (res == "")
                        throw new Exception("Item not found...");
                    string[] val = res.Split('%');
                    
                    //Converting string array to List using LINQ query//
                    var recs = (from data in val 
                                select data).ToList();

                    productNameTextBox.Text = recs[0];
                    categoryTextBox.Text = recs[1];
                    itemPriceTextBox.Text = recs[2];
                    noOfItemsTextBox.Text = recs[3];

                    productNameTextBox.Enabled = false;
                    categoryTextBox.Enabled = false;
                    imageTextBox.Enabled = false;

                    Image img = aProd.LoadImage(prodId);
                    pictureBox.Image = img;
                }

                else
                    MessageBox.Show("Please enter the Product ID..", "Error");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void updateStockButton_Click(object sender, EventArgs e)
        {
            if (productNameTextBox.Enabled == false)
            {
                EShoppingLibrary.Products aProd = new EShoppingLibrary.Products();

                double newPrice = Convert.ToDouble(itemPriceTextBox.Text);
                int newStock = Convert.ToInt32(noOfItemsTextBox.Text);
                int prodID = Convert.ToInt32(productIdTextBox.Text);

                int res = aProd.UpdateProduct(newPrice, newStock, prodID);
                if (res == 1)
                    MessageBox.Show("Update Successful...");
                else
                    MessageBox.Show(aProd.LastError, "Error");
            }
        }
    }
}
