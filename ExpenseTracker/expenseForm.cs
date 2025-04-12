using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using Expense_Tracker.Database;

namespace ExpenseTrackerApp
{
    public partial class expenseForm : Form
    {
        // Connection string to the SQLite database
        private static string dbPath = "Data Source=expenses.db";

        // Delegate and event for notifying when an expense is saved
        public delegate void ExpenseSavedHandler();
        public event ExpenseSavedHandler ExpenseSaved;

        // Constructor
        public expenseForm()
        {
            InitializeComponent();
            InitializeDatabase();              // Create database and table if not exists
            LoadCategoriesIntoComboBox();      // Load available categories into the combo box
            LoadExpenses();                    // Load existing expense records into the grid
        }

        // Method to initialize database and create table if it does not exist
        public static void InitializeDatabase()
        {
            if (!File.Exists("expenses.db"))
                SQLiteConnection.CreateFile("expenses.db");  // Create DB file if missing

            using (var conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Expenses (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Amount REAL,
                        Category TEXT,
                        Date TEXT,
                        Description TEXT
                    );";

                using (var cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();  // Execute the table creation command
                }
            }
        }

        // Load all categories into the category combo box
        private void LoadCategoriesIntoComboBox()
        {
            try
            {
                cmbCategory.Items.Clear();  // Clear previous items
                var dt = Categorizationdb.GetAllCategories();  // Get categories from DB

                foreach (DataRow row in dt.Rows)
                    cmbCategory.Items.Add(row["Name"].ToString());  // Add category name

                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;  // Select first item by default
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        // Clear all form input fields
        private void ClearForm()
        {
            txtAmount.Text = "";
            txtDescription.Text = "";
            datePicker.Value = DateTime.Today;

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;  // Reset combo box selection
        }

        // Load all expenses from database into the DataGridView
        private void LoadExpenses()
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string query = "SELECT * FROM Expenses ORDER BY Date DESC";  // Query to get all expenses
                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);  // Fill the table with query result
                        dgvExpenses.DataSource = table;  // Bind data to DataGridView
                    }
                }

                dgvExpenses.Columns["Id"].Visible = false;  // Hide ID column
                dgvExpenses.AutoResizeColumns();            // Adjust column sizes
                dgvExpenses.AutoResizeRows();               // Adjust row sizes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Add new expense to the database
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate amount input
                if (!double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                // Gather input values
                string category = cmbCategory.SelectedItem?.ToString() ?? "";
                string date = datePicker.Value.ToString("yyyy-MM-dd");
                string description = txtDescription.Text.Trim();

                // Insert new expense into database
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO Expenses (Amount, Category, Date, Description)
                        VALUES (@amount, @category, @date, @description);";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.ExecuteNonQuery();  // Execute insert command
                    }
                }

                MessageBox.Show("Expense added successfully!");
                ClearForm();       // Reset form fields
                LoadExpenses();    // Refresh expenses list
                ExpenseSaved?.Invoke();  // Fire saved event
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Edit selected expense
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to edit.");
                return;
            }

            try
            {
                var row = dgvExpenses.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);  // Get expense ID

                // Validate and gather input values
                if (!double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                string category = cmbCategory.SelectedItem?.ToString() ?? "";
                string date = datePicker.Value.ToString("yyyy-MM-dd");
                string description = txtDescription.Text.Trim();

                // Update the selected expense in database
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE Expenses
                        SET Amount = @amount,
                            Category = @category,
                            Date = @date,
                            Description = @description
                        WHERE Id = @id;";

                    using (var cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();  // Execute update command
                    }
                }

                MessageBox.Show("Expense updated successfully!");
                ClearForm();       // Reset form
                LoadExpenses();    // Reload updated data
                ExpenseSaved?.Invoke();  // Trigger event
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Delete selected expense from database
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to delete.");
                return;
            }

            var row = dgvExpenses.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["Id"].Value);  // Get selected expense ID

            // Confirm deletion
            if (MessageBox.Show("Are you sure you want to delete this expense?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new SQLiteConnection(dbPath))
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM Expenses WHERE Id = @id";  // SQL to delete

                        using (var cmd = new SQLiteCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();  // Execute delete command
                        }
                    }

                    MessageBox.Show("Expense deleted!");
                    ClearForm();       // Clear inputs
                    LoadExpenses();    // Refresh grid
                    ExpenseSaved?.Invoke();  // Trigger event
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting expense: " + ex.Message);
                }
            }
        }

        // Get the total amount of expenses for a given month and year
        public static decimal GetTotalExpensesForMonth(string month, string year)
        {
            decimal total = 0;

            using (var conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                string query = "SELECT SUM(Amount) FROM Expenses WHERE strftime('%m', Date) = @month AND strftime('%Y', Date) = @year";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month.PadLeft(2, '0'));  // Pad month with 0
                    cmd.Parameters.AddWithValue("@year", year);
                    var result = cmd.ExecuteScalar();  // Get total amount

                    total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;  // Handle null result
                }
            }

            return total;  // Return total expense for the month
        }

        // Event handler for txtAmount text change (empty for now)
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
