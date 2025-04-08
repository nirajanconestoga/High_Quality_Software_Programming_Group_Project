using System;
using System.Data;
using System.Windows.Forms;
using Expense_Tracker.Database;

namespace Expense_Tracker
{
    public partial class CategorizationForm : Form
    {
        private int selectedCategoryId = -1;

        public CategorizationForm()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Categorizationdb.InitializeDatabase();
            LoadCategories();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryInput.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Categorizationdb.AddCategory(categoryName);
            txtCategoryInput.Clear();
            selectedCategoryId = -1;
            LoadCategories();
        }

        private void LoadCategories()
        {
            DataTable dt = Categorizationdb.GetAllCategories();
            dgvCategoryList.DataSource = dt;
        }

        private void dgvCategoryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedCategoryId = Convert.ToInt32(dgvCategoryList.Rows[e.RowIndex].Cells["Id"].Value);
                txtCategoryInput.Text = dgvCategoryList.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string newName = txtCategoryInput.Text.Trim();

            if (selectedCategoryId == -1 || string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Select a category and enter a new name.");
                return;
            }

            Categorizationdb.UpdateCategory(selectedCategoryId, newName);
            txtCategoryInput.Clear();
            selectedCategoryId = -1;
            LoadCategories();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == -1)
            {
                MessageBox.Show("Please select a category to delete.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Categorizationdb.DeleteCategory(selectedCategoryId);
                txtCategoryInput.Clear();
                selectedCategoryId = -1;
                LoadCategories();
            }
        }

        private void lblCategory_Click(object sender, EventArgs e)
        {
        }
    }
}
