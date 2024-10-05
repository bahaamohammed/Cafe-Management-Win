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
        private Form previousForm;  // Declare a general Form reference
        public frmItems(Form previousForm = null)
        {
            InitializeComponent();
            this.previousForm = previousForm;
        }
        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.Close();
            }
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (previousForm is frmUserOrder)
            {
                previousForm.Show();
                this.Hide();
            }
            else if (previousForm != null)
            {
                previousForm.Close();
                frmUserOrder frmUserOrder = new frmUserOrder(false, this);
                frmUserOrder.Show(); // Show the main form as a dialog

                // Hide the login form and open the main form
                this.Hide();
            }
            
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (previousForm is frmUsers)
            {
                previousForm.Show();
                this.Hide();
            }
            else if (previousForm != null)
            {
                previousForm.Close();
                frmUsers frmUsers = new frmUsers(this);
                frmUsers.ShowDialog(); // Show the main form as a dialog

                // Hide the login form and open the main form
                this.Hide();
            }
           
        }

    }
}
