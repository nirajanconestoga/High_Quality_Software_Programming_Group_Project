using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace ExpenseTrackerApp
{
    public partial class Form1 : Form
    {
        string dbPath = "Data Source=expenses.db";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase(); // <- Create DB + Table
            LoadExpenses();       // <- Load data into grid
        }

        private void InitializeDatabase()
        {
            if (!File.Exists("expenses.db"))
            {
                SQLiteConnection.CreateFile("expenses.db");
            }

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

        private void ClearForm()
        {
            txtAmount.Text = "";
            txtCategory.Text = "";
            txtDescription.Text = "";
            datePicker.Value = DateTime.Today;
        }

        private void LoadExpenses()
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Expenses ORDER BY Date DESC";

                    using (var cmd = new SQLiteCommand(selectQuery, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        var table = new System.Data.DataTable();
                        adapter.Fill(table);
                        dgvExpenses.DataSource = table;
                    }
                }

                // Optional: auto-resize columns and headers
                dgvExpenses.Columns["Id"].Visible = false; // Hide ID column
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
                double amount;
                if (!double.TryParse(txtAmount.Text, out amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                string category = txtCategory.Text.Trim();
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
                LoadExpenses(); // Refresh grid
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
                // Get selected row values
                DataGridViewRow selectedRow = dgvExpenses.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                double amount;
                if (!double.TryParse(txtAmount.Text, out amount))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                string category = txtCategory.Text.Trim();
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

            DialogResult result = MessageBox.Show("Are you sure you want to delete this expense?",
                                                  "Confirm Delete",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataGridViewRow selectedRow = dgvExpenses.SelectedRows[0];
                    int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

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

                    MessageBox.Show("Expense deleted successfully!");
                    ClearForm();
                    LoadExpenses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting expense: " + ex.Message);
                }
            }
        }


    }
}

