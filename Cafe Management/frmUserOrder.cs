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
        public frmUserOrder(bool isGuest)
        {
            InitializeComponent();
            this.isGuest = isGuest;
            btnItems.Visible = !isGuest;
            btnUsers.Visible = !isGuest;
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            frmItems frmItems = new frmItems();
            frmItems.ShowDialog(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();

            // After the main form is closed (on logout), show the login form again
            this.Show();
        }
    }
}
