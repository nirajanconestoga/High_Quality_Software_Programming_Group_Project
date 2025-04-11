using ExpenseTracker.Database;
using System;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class Income_ManagementForm : Form
    {
        // Event delegate declaration to notify Dashboard when income is saved
        public delegate void IncomeSavedHandler(); // Concept: Delegates & Events
        public event IncomeSavedHandler IncomeSaved;

        // Constructor - Initializes form components
        public Income_ManagementForm()
        {
            InitializeComponent();
        }

        // Event handler for form load
        private void Form1_Load(object sender, EventArgs e)
        {
            // Concept: Initialization of external database logic
            income_managementdb.InitializeDatabase();
            LoadIncomeData(); // Load data into DataGridView when form loads
        }

        // Load data from DB into DataGridView
        private void LoadIncomeData()
        {
            // Concept: Data Binding - connecting DB data to UI
            dgvIncome.DataSource = income_managementdb.GetIncomeList();
        }

        // Button click handler to add new income entry
        private void btnAddIncome_Click(object sender, EventArgs e)
        {
            try
            {
                // Concept: Input validation
                if (string.IsNullOrWhiteSpace(txtSource.Text) || string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    MessageBox.Show("Please enter both source and amount.");
                    return;
                }

                // Concept: Data type parsing and validation
                if (!double.TryParse(txtAmount.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid numeric amount.");
                    return;
                }

                // Create new income object
                var income = new IncomeEntry(
                    txtSource.Text,
                    amount,
                    dtpDate.Value
                );

                // Concept: Database interaction to add data
                income_managementdb.AddIncome(income.Source, income.Amount, income.Date);

                LoadIncomeData(); // Refresh displayed data
                ClearInputs(); // Reset input fields

                IncomeSaved?.Invoke(); // Concept: Event invocation (if there are subscribers)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding income: " + ex.Message);
            }
        }

        // Button click handler to update selected income entry
        private void btnUpdateIncome_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a row is selected
                if (dgvIncome.SelectedRows.Count > 0)
                {
                    // Create income object with ID from selected row
                    var income = new IncomeEntry(
                        Convert.ToInt32(dgvIncome.SelectedRows[0].Cells["Id"].Value),
                        txtSource.Text,
                        double.Parse(txtAmount.Text),
                        dtpDate.Value
                    );

                    // Concept: Database interaction for update
                    income_managementdb.UpdateIncome(income.Id, income.Source, income.Amount, income.Date);

                    LoadIncomeData(); // Refresh table
                    ClearInputs(); // Clear form inputs

                    IncomeSaved?.Invoke(); // Notify any subscribers
                }
                else
                {
                    MessageBox.Show("Please select an income entry to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating income: " + ex.Message);
            }
        }

        // Button click handler to delete selected income entry
        private void btnDeleteIncome_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIncome.SelectedRows.Count > 0)
                {
                    // Concept: Retrieving values from selected DataGridView row
                    int id = Convert.ToInt32(dgvIncome.SelectedRows[0].Cells["Id"].Value);

                    // Delete income from database
                    income_managementdb.DeleteIncome(id);

                    LoadIncomeData(); // Refresh data
                    ClearInputs(); // Clear input fields

                    IncomeSaved?.Invoke(); // Notify Dashboard
                }
                else
                {
                    MessageBox.Show("Please select an income entry to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting income: " + ex.Message);
            }
        }

        // DataGridView cell click - load selected row's data into form fields
        private void dgvIncome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Concept: Row index check to prevent header click errors
            if (e.RowIndex >= 0)
            {
                // Concept: Populate fields from selected row for editing
                DataGridViewRow row = dgvIncome.Rows[e.RowIndex];
                txtSource.Text = row.Cells["Source"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                dtpDate.Value = DateTime.Parse(row.Cells["Date"].Value.ToString());
            }
        }

        // Utility method to clear all input fields
        private void ClearInputs()
        {
            txtSource.Text = "";
            txtAmount.Text = "";
            dtpDate.Value = DateTime.Today;
        }

        // Optional handler: can be used for live validation, formatting, etc.
        private void txtSource_TextChanged(object sender, EventArgs e)
        {
        }

        // Placeholder event handler for future feature
        private void dgvIncome_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Optional handler for live validation on amount textbox
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
        }

        // Placeholder for label click event, unused
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }

    // Model class representing a single income entry
    public class IncomeEntry
    {
        // Properties map directly to database fields
        public int Id { get; set; }
        public string Source { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        // Constructor for new entries (no ID yet)
        public IncomeEntry(string source, double amount, DateTime date)
        {
            Source = source;
            Amount = amount;
            Date = date;
        }

        // Constructor for existing entries (ID present)
        public IncomeEntry(int id, string source, double amount, DateTime date)
        {
            Id = id;
            Source = source;
            Amount = amount;
            Date = date;
        }
    }
}
