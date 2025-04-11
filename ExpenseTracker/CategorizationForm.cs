using System;
using System.Data;
using System.Windows.Forms;
using Expense_Tracker.Database;

namespace Expense_Tracker
{
    public partial class CategorizationForm : Form
    {
        // Tracks the selected category's ID — used for update/delete operations
        // Concept: State Management
        private int selectedCategoryId = -1;

        public CategorizationForm()
        {
            InitializeComponent();

            // Attaches the Load event to its handler
            // Concept: Event-Driven Programming
            Load += Form1_Load;
        }

        // Form Load event initializes database and loads categories
        // Concept: Initialization Logic + Event Binding
        private void Form1_Load(object sender, EventArgs e)
        {
            Categorizationdb.InitializeDatabase(); // Setup DB if not exists
            LoadCategories(); // Load existing categories into UI
        }

        // Handles adding a new category to the database
        // Concept: Input Validation, Data Persistence, UI Feedback
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryInput.Text.Trim();

            // Simple input validation
            // Concept: Defensive Programming
            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Concept: Separation of Concerns (calling DB logic via data access layer)
            Categorizationdb.AddCategory(categoryName);

            // Reset input state and reload
            txtCategoryInput.Clear();
            selectedCategoryId = -1;
            LoadCategories(); // Refresh data grid after adding
        }

        // Loads all categories into the DataGridView
        // Concept: Data Binding
        private void LoadCategories()
        {
            DataTable dt = Categorizationdb.GetAllCategories();

            // Simple one-way binding of data to UI control
            dgvCategoryList.DataSource = dt;
        }

        // Populates selected category data into input box on DataGridView row click
        // Concept: UI Event Handling + State Management
        private void dgvCategoryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Gets ID and Name of selected row
                selectedCategoryId = Convert.ToInt32(dgvCategoryList.Rows[e.RowIndex].Cells["Id"].Value);
                txtCategoryInput.Text = dgvCategoryList.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            }
        }

        // Updates the selected category with a new name
        // Concept: CRUD Operation (Update), Input Validation, Reusability
        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string newName = txtCategoryInput.Text.Trim();

            // Validate selection and input
            if (selectedCategoryId == -1 || string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Select a category and enter a new name.");
                return;
            }

            // Concept: Separation of Concerns
            Categorizationdb.UpdateCategory(selectedCategoryId, newName);

            // Reset and reload
            txtCategoryInput.Clear();
            selectedCategoryId = -1;
            LoadCategories();
        }

        // Deletes the selected category after confirmation
        // Concept: Confirmation Dialogs + CRUD Operation (Delete)
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == -1)
            {
                MessageBox.Show("Please select a category to delete.");
                return;
            }

            // Prompt user before deleting
            var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Categorizationdb.DeleteCategory(selectedCategoryId);

                // Reset and refresh UI
                txtCategoryInput.Clear();
                selectedCategoryId = -1;
                LoadCategories();
            }
        }

        // Empty placeholder event — could be used for future UI logic
        private void lblCategory_Click(object sender, EventArgs e)
        {
        }

        // Placeholder for reacting to text changes if needed
        private void txtCategoryInput_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
