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
        UserManager userManager;
        int userId = 0;
        string userName = "";
        public frmUsers()
        {
            InitializeComponent();
            userManager = new UserManager();
        }
        
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
                if (userManager.AddUser(username, password, phone))
                {
                    MessageBox.Show("User added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Insert failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please check your entries", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                userId = int.Parse(row.Cells["id"].Value.ToString());
                txtUsername.Text = row.Cells["username"].Value.ToString();
                userName = txtUsername.Text;
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                if (userManager.DeleteUser(userId))
                {
                    MessageBox.Show("User deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Delete failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                if (userManager.UpdateUser(userId, txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtPhone.Text.Trim()))
                {
                    MessageBox.Show("User updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = userManager.LoadUsers();
            dgvUsers.Columns["id"].Visible = false;
            dgvUsers.Columns[1].HeaderText = "Username";
            dgvUsers.Columns[2].HeaderText = "Phone";
            dgvUsers.Columns[3].HeaderText = "Password";
        }
        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            userId = 0;
            userName = "";
        }
    }
}
