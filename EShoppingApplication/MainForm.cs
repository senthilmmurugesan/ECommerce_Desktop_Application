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
    public partial class MainForm : Form
    {
        frmHomePage homeForm = null;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            loginSignupToolStripMenuItem.Enabled = false;
            loginSignupToolStripMenuItem.Enabled = true;
        }

        public void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddProduct addProd = new frmAddProduct();
            addProd.MdiParent = this;
            addProd.WindowState = FormWindowState.Maximized;
            addProd.Show();
        }

        public void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homeForm = new frmHomePage();
            homeForm.MdiParent = this;
            homeForm.WindowState = FormWindowState.Maximized;
            homeForm.Show();
        }

        public void loginSignupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin loginForm = new frmLogin();
            loginForm.MdiParent = this;
            loginForm.WindowState = FormWindowState.Maximized;
            loginForm.Show();
        }

        private void loginSignupToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            if (loginSignupToolStripMenuItem.Enabled == true)
            {
                addItemToolStripMenuItem.Enabled = false;
                homeToolStripMenuItem.Enabled = false;
                signoutToolStripMenuItem.Enabled = false;
                myCartToolStripMenuItem.Enabled = false;
            }
            else
            {
                addItemToolStripMenuItem.Enabled = true;
                homeToolStripMenuItem.Enabled = true;
                signoutToolStripMenuItem.Enabled = true;
                myCartToolStripMenuItem.Enabled = true;
            }
        }

        private void signoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginSignupToolStripMenuItem.Enabled = false;
            loginSignupToolStripMenuItem.Enabled = true;
            Product.allProducts.Clear();
            foreach (Form frm in this.MdiChildren)
                frm.Close();
        }

        private void myCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCart aCartItem = new frmCart();
            aCartItem.MdiParent = this;
            aCartItem.WindowState = FormWindowState.Maximized;
            aCartItem.Show();
        }

        public void EnableMenuItems(string username)
        {
            if (username != "admin")
            {
                loginSignupToolStripMenuItem.Enabled = false;
                addItemToolStripMenuItem.Enabled = false;
            }
            else
            {
                loginSignupToolStripMenuItem.Enabled = false;
                myCartToolStripMenuItem.Enabled = false;
            }
        }
    }
}
