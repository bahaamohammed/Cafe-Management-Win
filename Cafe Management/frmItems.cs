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
    public partial class frmItems : Form
    {
        public frmItems()
        {
            InitializeComponent();
        }
        DatabaseManager dbManager = new DatabaseManager();
        int itemId = 0;
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

        private void frmItems_Load(object sender, EventArgs e)
        {
            LoadItems();
        }
        private void LoadItems()
        {
            dgvItems.DataSource = null;
            string query = "SELECT id, name, cat, price FROM items"; // Adjust column names as needed
            DataTable dataTable = dbManager.ExecuteQuery(query); // Assuming ExecuteQuery returns a DataTable

            if (dataTable != null)
            {
                dgvItems.DataSource = dataTable; // Set the DataGridView's DataSource
                if (dgvItems.Columns.Count > 0)
                {
                    dgvItems.Columns["id"].Visible = false;
                    dgvItems.Columns[1].HeaderText = "Name";
                    dgvItems.Columns[2].HeaderText = "Category";
                    dgvItems.Columns[3].HeaderText = "Price";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            int itemPrice = int.Parse(txtItemPrice.Text.Trim());
            string itemCat = cmbCategory.Text.Trim();
            if (itemName.Length > 0 && itemPrice > 0 && itemCat.Length > 0)
            {
                addItem(itemName, itemCat, itemPrice);
            }
            else
            {
                MessageBox.Show("Please Check Your Entries", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addItem(string itemName, string itemCat, int itemPrice)
        {
            string insertQuery = "INSERT INTO items (name, cat, price) VALUES (@ItemName, @ItemCat, @ItemPrice)";
            OleDbParameter[] parameters = {
                new OleDbParameter("@ItemName", itemName),
                new OleDbParameter("@ItemCat", itemCat),
                new OleDbParameter("@ItemPrice", itemPrice)
            };

            int result = dbManager.ExecuteNonQuery(insertQuery, parameters);

            if (result > 0)
            {
                txtItemName.Clear();
                txtItemPrice.Clear();
                cmbCategory.SelectedIndex = -1;
                MessageBox.Show("Item inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Insert failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateItem()
        {
            string updateQuery = "UPDATE items SET name = @Name, cat = @Cat, price = @Price WHERE id = @ItemId";
            OleDbParameter[] parameters = {
                new OleDbParameter("@Name", txtItemName.Text.Trim()),
                new OleDbParameter("@Cat", cmbCategory.Text.Trim()),
                new OleDbParameter("@Price", txtItemPrice.Text.Trim()),
                new OleDbParameter("@ItemId", itemId),
            };

            int result = dbManager.ExecuteNonQuery(updateQuery, parameters);

            if (result > 0)
            {
                txtItemName.Clear();
                cmbCategory.SelectedIndex = -1;
                txtItemPrice.Clear();
                LoadItems();
                MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Updated failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                updateItem();
            }
            else
            {
                MessageBox.Show("Please select the item you need to update it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteItem()
        {
            string deleteQuery = "Delete from items where id = @ItemId";
            OleDbParameter[] parameters = {
                new OleDbParameter("@ItemId", itemId)
            };

            int result = dbManager.ExecuteNonQuery(deleteQuery, parameters);

            if (result > 0)
            {
                txtItemName.Clear();
                cmbCategory.SelectedIndex = -1;
                txtItemPrice.Clear();
                LoadItems();
                MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Deleted failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                deleteItem();
            }
            else
            {
                MessageBox.Show("Please select the item you need to delete it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index is selected
            {
                DataGridViewRow row = dgvItems.Rows[e.RowIndex];
                itemId = int.Parse(row.Cells["id"].Value.ToString());
                txtItemName.Text = row.Cells["name"].Value.ToString();
                int index = cmbCategory.FindStringExact(row.Cells["cat"].Value.ToString());
                if (index != -1) // If the item is found
                {
                    cmbCategory.SelectedIndex = index; // Select the item
                }
                txtItemPrice.Text = row.Cells["price"].Value.ToString();
            }
        }
    }
}
