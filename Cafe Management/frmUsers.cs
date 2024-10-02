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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder();
            frmUserOrder.ShowDialog(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();

            // After the main form is closed (on logout), show the login form again
            this.Show();

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmUsers frmUsers = new frmUsers();
            frmUsers.ShowDialog(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();

            // After the main form is closed (on logout), show the login form again
            this.Show();
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
