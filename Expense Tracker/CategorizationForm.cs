using System;
using System.Data;
using System.Windows.Forms;
using Expense_Tracker.Database;

namespace Expense_Tracker
{
    public partial class CategorizationForm : Form
    {
        public CategorizationForm()
        {
            InitializeComponent();
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

            // Uses the overloaded method with just category name
            Categorizationdb.AddCategory(categoryName);

            txtCategoryInput.Clear();
            LoadCategories();
        }

        private void LoadCategories()
        {
            DataTable dt = Categorizationdb.GetAllCategories();
            dgvCategoryList.DataSource = dt;
        }
    }
}
