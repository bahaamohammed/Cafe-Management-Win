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

        private void lnkGuest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder(true);
            frmUserOrder.ShowDialog(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();

            // After the main form is closed (on logout), show the login form again
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder();
            frmUserOrder.ShowDialog(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();

            // After the main form is closed (on logout), show the login form again
            this.Show();

        }
        private void connectDB()
        {
            // Connection string to connect to the Access database
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\\moh_for_emerg.accdb;Persist Security Info=False;";

            // Create a connection to the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    MessageBox.Show("Connection to the database was successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Always close the connection when done
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            connectDB();
        }
    }
}
