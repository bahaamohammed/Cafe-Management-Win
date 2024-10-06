using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Cafe_Management
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        DatabaseManager dbManager = new DatabaseManager();
        private void lnkGuest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder(true);
            frmUserOrder.Show(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Get the entered username and password
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Call the login method
            if (Login(username, password))
            {
                frmUserOrder frmUserOrder = new frmUserOrder(false);
                frmUserOrder.Show(); // Show the main form as a dialog

                // Hide the login form and open the main form
                this.Hide();

            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

          

        }
        // Method to handle login validation
        private bool Login(string username, string password)
        {
            // SQL Query to check for matching username and password
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
            OleDbParameter[] parameters = {
                new OleDbParameter("@Username", username),
                new OleDbParameter("@Password", password)
            };

            // Execute the query and check if any record is found
            DataTable result = dbManager.ExecuteQuery(query, parameters);

            // Check if the login was successful (if at least 1 row is returned)
            if (result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0][0]) > 0)
            {
                return true; // Login successful
            }
            return false; // Login failed
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
