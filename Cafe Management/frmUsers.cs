using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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

        DatabaseManager dbManager = new DatabaseManager();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string phone = txtPhone.Text.Trim();
            if (username.Length > 0 && password.Length > 0 && phone.Length > 0)
            {
                addUser(username, password, phone);
            }
            else
            {
                MessageBox.Show("Please Check Your Entries", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addUser(string username, string password, string phone)
        {
            string insertQuery = "INSERT INTO users (username, phone, [password]) VALUES (@Username, @Phone, @Password)";
            OleDbParameter[] parameters = {
                new OleDbParameter("@Username", username),
                new OleDbParameter("@Phone", phone),
                new OleDbParameter("@Password", password)
            };

            int result = dbManager.ExecuteNonQuery(insertQuery, parameters);

            if (result > 0)
            {
                txtPassword.Clear();
                txtPhone.Clear();
                txtUsername.Clear();
                MessageBox.Show("User inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Insert failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
