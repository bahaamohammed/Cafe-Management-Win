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
        ItemManager itemManager;
        int _price, qty, _total;
        int num = 0;
        int flag = 0;
        string _name, _cat;
        DataTable dt = new DataTable();
        public frmUserOrder(bool isGuest = false)
        {
            InitializeComponent();
            this.isGuest = isGuest;
            btnItems.Visible = !isGuest;
            btnUsers.Visible = !isGuest;
            itemManager = new ItemManager();
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();

        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            frmItems frmItems = new frmItems();
            frmItems.Show(); // Show the main form as a dialog
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

        private void LoadItems()
        {
            dgvItems.DataSource = itemManager.LoadItems();
            dgvItems.Columns["id"].Visible = false;
            dgvItems.Columns[1].HeaderText = "Name";
            dgvItems.Columns[2].HeaderText = "Category";
            dgvItems.Columns[3].HeaderText = "Price";
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("What is the Quantity of item?");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the product to be ordered");
            }
            else
            {
                num += 1;
                _total = _price * int.Parse(txtQuantity.Text);
                dt.Rows.Add(num, _name, _cat, _price, _total);
                dgvOrders.DataSource = dt;
                flag = 0;
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _name = dgvItems.SelectedRows[0].Cells[1].Value.ToString();
            _cat    = dgvItems.SelectedRows[0].Cells[2].Value.ToString();
            _price  = int.Parse(dgvItems.SelectedRows[0].Cells[3].Value.ToString());
            flag    = 1;
        }

        private void frmUserOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void frmUserOrder_Load(object sender, EventArgs e)
        {
            LoadItems();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Total", typeof(int));
            dgvOrders.DataSource = dt;
        }
    }
}
