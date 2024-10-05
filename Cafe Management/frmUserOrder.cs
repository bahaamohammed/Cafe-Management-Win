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
    public partial class frmUserOrder : Form
    {
        bool isGuest = false;
        private Form previousForm;  // Declare a general Form reference
        public frmUserOrder(bool isGuest = false, Form previousForm = null)
        {
            InitializeComponent();
            this.isGuest = isGuest;
            btnItems.Visible = !isGuest;
            btnUsers.Visible = !isGuest;
            this.previousForm = previousForm;
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (previousForm is frmLogin)
            {
                previousForm.Show();
                this.Close();
            }
            else if(previousForm != null)
            {
                previousForm.Close();
                this.Close();
            }
            
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            if (previousForm is frmItems)
            {
                previousForm.Show();
                this.Hide();
            }
            else if (previousForm != null)
            {
                if (!(previousForm is frmLogin))
                {
                    previousForm.Close();
                }
                frmItems frmItems = new frmItems(this);
                frmItems.Show(); // Show the main form as a dialog
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
                if (!(previousForm is frmLogin))
                {
                    previousForm.Close();
                }
                frmUsers frmUsers = new frmUsers(this);
                frmUsers.Show(); // Show the main form as a dialog
                // Hide the login form and open the main form
                this.Hide();
            }

        }

       
    }
}
