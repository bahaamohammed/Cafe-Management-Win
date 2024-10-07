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
        int userId = 0;
        string userName = "";

        DatabaseManager dbManager = new DatabaseManager();
        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmUserOrder frmUserOrder = new frmUserOrder(false);
            frmUserOrder.Show(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();
        }

       

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
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
                LoadUsers();
                MessageBox.Show("User inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Insert failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            dgvUsers.DataSource = null;
            string query = "SELECT id, username, phone, [password] FROM users"; // Adjust column names as needed
            DataTable dataTable = dbManager.ExecuteQuery(query); // Assuming ExecuteQuery returns a DataTable

            if (dataTable != null)
            {
                dgvUsers.DataSource = dataTable; // Set the DataGridView's DataSource
            }
            dgvUsers.Columns["id"].Visible = false;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index is selected
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                userId = int.Parse(row.Cells["id"].Value.ToString());
                txtUsername.Text = row.Cells["username"].Value.ToString();
                userName = txtUsername.Text;
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString(); // Make sure to handle passwords securely
            }
        }

        private void frmUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            frmItems frmItems = new frmItems();
            frmItems.Show(); // Show the main form as a dialog

            // Hide the login form and open the main form
            this.Hide();
        }

        private void deleteUser()
        {
            string insertQuery = "Delete from users where id = @UserId";
            OleDbParameter[] parameters = {
                new OleDbParameter("@UserId", userId)
            };

            int result = dbManager.ExecuteNonQuery(insertQuery, parameters);

            if (result > 0)
            {
                txtPassword.Clear();
                txtPhone.Clear();
                txtUsername.Clear();
                LoadUsers();
                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Deleted failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                if (userName == DatabaseManager.currentUsername)
                {
                    MessageBox.Show("You cannot delete your user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    deleteUser();
                }
            }
            else
            {
                MessageBox.Show("Please select the user you need to delete it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
