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
        private static string dbPath = "Data Source=expenses.db";

        public delegate void ExpenseSavedHandler();
        public event ExpenseSavedHandler ExpenseSaved;

        public expenseForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadCategoriesIntoComboBox();
            LoadExpenses();
        }

        public static void InitializeDatabase()
        {
            if (!File.Exists("expenses.db"))
                SQLiteConnection.CreateFile("expenses.db");

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
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadCategoriesIntoComboBox()
        {
            try
            {
                cmbCategory.Items.Clear();
                var dt = Categorizationdb.GetAllCategories();

                foreach (DataRow row in dt.Rows)
                    cmbCategory.Items.Add(row["Name"].ToString());

                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtAmount.Text = "";
            txtDescription.Text = "";
            datePicker.Value = DateTime.Today;

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void LoadExpenses()
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string query = "SELECT * FROM Expenses ORDER BY Date DESC";
                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dgvExpenses.DataSource = table;
                    }
                }

                dgvExpenses.Columns["Id"].Visible = false;
                dgvExpenses.AutoResizeColumns();
                dgvExpenses.AutoResizeRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                string category = cmbCategory.SelectedItem?.ToString() ?? "";
                string date = datePicker.Value.ToString("yyyy-MM-dd");
                string description = txtDescription.Text.Trim();

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
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Expense added successfully!");
                ClearForm();
                LoadExpenses();
                ExpenseSaved?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to edit.");
                return;
            }

            try
            {
                var row = dgvExpenses.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);

                if (!double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                string category = cmbCategory.SelectedItem?.ToString() ?? "";
                string date = datePicker.Value.ToString("yyyy-MM-dd");
                string description = txtDescription.Text.Trim();

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
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Expense updated successfully!");
                ClearForm();
                LoadExpenses();
                ExpenseSaved?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to delete.");
                return;
            }

            var row = dgvExpenses.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["Id"].Value);

            if (MessageBox.Show("Are you sure you want to delete this expense?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new SQLiteConnection(dbPath))
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM Expenses WHERE Id = @id";
                        using (var cmd = new SQLiteCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Expense deleted!");
                    ClearForm();
                    LoadExpenses();
                    ExpenseSaved?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting expense: " + ex.Message);
                }
            }
        }

        public static decimal GetTotalExpensesForMonth(string month, string year)
        {
            decimal total = 0;

            using (var conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                string query = "SELECT SUM(Amount) FROM Expenses WHERE strftime('%m', Date) = @month AND strftime('%Y', Date) = @year";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month.PadLeft(2, '0'));
                    cmd.Parameters.AddWithValue("@year", year);
                    var result = cmd.ExecuteScalar();
                    total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            return total;
        }
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
