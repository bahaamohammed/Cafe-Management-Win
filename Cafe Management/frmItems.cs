using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_Management
{
    public partial class frmItems : Form
    {
        public frmItems()
        {
            InitializeComponent();
        }
        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder(false);
            frmUserOrder.Show(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmUsers frmUsers = new frmUsers();
            frmUsers.Show(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();
        }

        private void frmItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
