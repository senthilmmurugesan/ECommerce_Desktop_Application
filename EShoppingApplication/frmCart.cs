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
    public partial class frmCart : Form
    {
        double[] totalPrice = new double[6];
        List<Product> allProducts = Product.allProducts;

        public frmCart()
        {
            InitializeComponent();
        }

        private void frmCart_Load(object sender, EventArgs e)
        {
            cartDataGridView.AllowUserToAddRows = false;
            cartDataGridView.ColumnCount = 2;
            cartDataGridView.Columns[0].Name = "Name";
            cartDataGridView.Columns[1].Name = "Price";

            foreach (Product aProd in allProducts)
            {    
                string[] row = { aProd.ItemName, aProd.ItemPrice.ToString()};
                cartDataGridView.Rows.Add(row);          
            }

            DataGridViewComboBoxColumn comboBox = new DataGridViewComboBoxColumn();
            cartDataGridView.Columns.Add(comboBox);
            comboBox.HeaderText = "Quantity";
            //comboBox.DefaultCellStyle.NullValue = "1";
            for (int i = 1; i < 6; i++)
                comboBox.Items.Add(i.ToString());

            foreach (DataGridViewColumn dc in cartDataGridView.Columns)
                if (dc.Index.Equals(2) )
                    dc.ReadOnly = false;
                else
                    dc.ReadOnly = true;

            DeleteDup();            
            cartDataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(cartDataGridView_EditingControlShowing);
        }

        private void cartDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double totPrice = 0;
            var comboBox = (DataGridViewComboBoxEditingControl)sender;
            int rowIndex = comboBox.EditingControlRowIndex;
            string oldPrice = cartDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            ComboBox cb = (ComboBox)sender;
            string quantity = cb.Text;

            totalPrice[rowIndex] = Convert.ToDouble(oldPrice) * Convert.ToDouble(quantity);
            foreach (double val in totalPrice)
                totPrice += val;

            totalLabel.Text = "Total Amount: $" + totPrice.ToString();
        }

        private void placeOrderButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            int cartCount = cartDataGridView.RowCount;
            if (cartCount != 0)
            {
                int quanCount = 0;
                foreach (double val in totalPrice)
                    if (val != 0) quanCount++;
                if (cartCount == quanCount)
                {
                    EShoppingLibrary.Products aProd = new EShoppingLibrary.Products();

                    foreach (DataGridViewRow row in cartDataGridView.Rows)
                    {
                        string name = row.Cells[0].Value.ToString();
                        int noOfItems = Convert.ToInt32(row.Cells[2].Value);
                        //MessageBox.Show(":" + name + ":" + noOfItems.ToString() + ":");
                        int res = aProd.DecrementStock(name, noOfItems);
                        if (res == 1)
                            count++;
                        else
                        {
                            MessageBox.Show(aProd.LastError, "Error");
                            clearCartButton_Click(sender, e);
                            break;
                        }
                        if (count == cartCount)
                        {
                            MessageBox.Show("Your order has been successfully placed..");
                            clearCartButton_Click(sender, e);
                            totalLabel.Text = "";
                            Product.allProducts.Clear();
                        }
                    }
                }
                else
                    MessageBox.Show("Please select the quantity...");
            }
            else
                MessageBox.Show("Your Cart is empty...");
        }

        private void DeleteDup()
        {
            for (int currentRow = 0; currentRow < cartDataGridView.Rows.Count; currentRow++)
            {
               var rowToCompare = cartDataGridView.Rows[currentRow]; 
               foreach (DataGridViewRow row in cartDataGridView.Rows)
               {  
                   if (rowToCompare.Equals(row)) continue; 
                   bool duplicateRow = true;
                   for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                       if ((null != rowToCompare.Cells[cellIndex].Value) && 
                           (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value)))
                      {
                         duplicateRow = false;
                         break;
                      }

                   if (duplicateRow)
                       cartDataGridView.Rows.Remove(row);
               }
            }
        }

        private void clearCartButton_Click(object sender, EventArgs e)
        {
            cartDataGridView.DataSource = null;
            cartDataGridView.Rows.Clear();
            totalLabel.Text = "";
            Product.allProducts.Clear();
        }
    }
}
