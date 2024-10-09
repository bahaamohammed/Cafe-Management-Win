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
        ItemManager itemManager;
        int itemId = 0;
        public frmItems()
        {
            InitializeComponent();
            itemManager = new ItemManager();
        }
        
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            int itemPrice = int.Parse(txtItemPrice.Text.Trim());
            string itemCat = cmbCategory.Text.Trim();
            if (itemName.Length > 0 && itemPrice > 0 && itemCat.Length > 0)
            {
                if (itemManager.addItem(itemName, itemCat, itemPrice))
                {
                    MessageBox.Show("Item added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadItems();
                }
                else
                {
                    MessageBox.Show("Insert failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Check Your Entries", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                if (itemManager.updateItem(itemId, txtItemName.Text.Trim(), cmbCategory.Text.Trim(), txtItemPrice.Text.Trim()))
                {
                    MessageBox.Show("Item updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadItems();
                }
                else
                {
                    MessageBox.Show("Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a item to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                if (itemManager.deleteItem(itemId))
                {
                    MessageBox.Show("Item deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadItems();
                }
                else
                {
                    MessageBox.Show("Delete failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a item to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void LoadItems()
        {
            dgvItems.DataSource = itemManager.LoadItems();
            dgvItems.Columns["id"].Visible = false;
            dgvItems.Columns[1].HeaderText = "Name";
            dgvItems.Columns[2].HeaderText = "Category";
            dgvItems.Columns[3].HeaderText = "Price";
        }
        private void ClearForm()
        {
            txtItemName.Clear();
            txtItemPrice.Clear();
            cmbCategory.SelectedIndex = -1;
            itemId = 0;
        }
    }
}
