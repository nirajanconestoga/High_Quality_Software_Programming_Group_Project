using System;
using System.Windows.Forms;
using ExpenseTracker.Database;

namespace ExpenseTracker
{
    // Delegate declaration
    public delegate void BudgetSavedHandler(string month, string year, decimal limit);

    public partial class Budget : Form
    {
        // Event based on the delegate
        public event BudgetSavedHandler BudgetSaved;

        public Budget()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database.Budgetdb.InitializeDatabase();
            cmbMonth.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string month = cmbMonth.Text;
            string year = txtYear.Text.Trim();
            string limitText = txtLimit.Text.Trim();

            if (string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(limitText))
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(limitText, out decimal limit))
            {
                MessageBox.Show("Invalid limit amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = Database.Budgetdb.SaveMonthlyBudget(month, year, limit);
            lblStatus.Text = success ? "✅ Budget saved successfully!" : "❌ Failed to save budget.";

            // Fire the delegate event only if budget is saved
            if (success)
            {
                BudgetSaved?.Invoke(month, year, limit);
            }
        }
    }
}
