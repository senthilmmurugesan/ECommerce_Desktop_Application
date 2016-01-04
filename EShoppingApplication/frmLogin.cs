using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using EShoppingLibrary;

namespace EShoppingApplication
{
    public partial class frmLogin : Form
    {
        Administrator anAdmin;
        Customer aCus;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            EShoppingLibrary.Customers aCustomer = new EShoppingLibrary.Customers();

            try
            {
                string username = newUsernameBox.Text;

                string password = newPasswordBox.Text;
                if (password != confirmPasswordBox.Text)
                    throw new Exception("Please check that your passwords match and try again.");

                string email = emailTextBox.Text;
                string name = nameTextBox.Text;
                string address = addressTextBox.Text;
                string city = cityTextBox.Text;
                int zipcode = Convert.ToInt32(zipcodeTextBox.Text);
                long phNum = Convert.ToInt64(phNumTextBox.Text);

                //Validating the customer details
                try
                {
                    if(username == "admin")
                        anAdmin = new Administrator(username, password, email, name);
                    else
                        aCus = new Customer(username, password, email, name, address, city, zipcode, phNum);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                //Adding the customer
                int res = aCustomer.Add(username, password, email, name, address, city, zipcode, phNum);

                if (res == 1)
                {
                    MessageBox.Show("Account created...Please Login now !!!", "Success");
                    ClearFields();
                }
                else
                    MessageBox.Show(aCustomer.LastError);
            }

            catch (FormatException fe)
            {
                MessageBox.Show("Zipcode and Phone Number should be numerals...", "Errors");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            EShoppingLibrary.Customers aCus = new EShoppingLibrary.Customers();
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            int res = aCus.ValidateCustomer(username, password);
            if (res == 1)
            {
                Program.currentMainForm.EnableMenuItems(username);
                frmHomePage home = new frmHomePage();
                home.MdiParent = this.MdiParent;
                home.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please check your username and password provided...");
        }

        private void newUsernameBox_TextChanged(object sender, EventArgs e)
        {
            EShoppingLibrary.Customers aCus = new EShoppingLibrary.Customers();

            string username = newUsernameBox.Text;
            if (username.Trim() != "" & username.All(Char.IsLetterOrDigit) & username.Length > 4)
            {
                int res = aCus.CheckAvailability(username);
                if (res == 1)
                {
                    checkUsernameLabel.Text = "Username not available";
                    checkUsernameLabel.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    checkUsernameLabel.Text = "Username available";
                    checkUsernameLabel.ForeColor = System.Drawing.Color.Green;
                }
            }

            else
                checkUsernameLabel.Text = "";
        }

        private void ClearFields()
        {
            newUsernameBox.Text = "";
            newPasswordBox.Text = "";
            confirmPasswordBox.Text = "";
            emailTextBox.Text = "";
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            cityTextBox.Text = "";
            zipcodeTextBox.Text = "";
            phNumTextBox.Text = "";
        }
    }
}
