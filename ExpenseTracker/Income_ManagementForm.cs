using ExpenseTracker.Database;

namespace ExpenseTracker
{
    // First class: Required for WinForms Designer to load correctly
    // Event-driven programming: Inherits from Form
    public partial class Income_ManagementForm : Form
    {
        public Income_ManagementForm()
        {
            InitializeComponent(); // Designer-initialized UI components
        }
        // Event handler for form load
        private void Form1_Load(object sender, EventArgs e)
        {
            income_managementdb.InitializeDatabase(); // DB setup
            LoadIncomeData();                         // Load data into grid
        }
        // Data Binding: Populate DataGridView with income records
        private void LoadIncomeData()
        {
            dgvIncome.DataSource = income_managementdb.GetIncomeList();
        }
        // Constructor + Encapsulation used to create object
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var income = new IncomeEntry(
                    txtSource.Text,
                    double.Parse(txtAmount.Text),
                    dtpDate.Value
                );
                // Encapsulated values passed to DB method
                income_managementdb.AddIncome(income.Source, income.Amount, income.Date);
                LoadIncomeData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding income: " + ex.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIncome.SelectedRows.Count > 0)
                {
                    var income = new IncomeEntry(
                        Convert.ToInt32(dgvIncome.SelectedRows[0].Cells["Id"].Value),
                        txtSource.Text,
                        double.Parse(txtAmount.Text),
                        dtpDate.Value
                    );

                    income_managementdb.UpdateIncome(income.Id, income.Source, income.Amount, income.Date);
                    LoadIncomeData();
                    ClearInputs();
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIncome.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvIncome.SelectedRows[0].Cells["Id"].Value);
                    income_managementdb.DeleteIncome(id);
                    LoadIncomeData();
                    ClearInputs();
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
        // Event-driven: triggered when a row in DataGridView is clicked
        private void dgvIncome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvIncome.Rows[e.RowIndex];
                txtSource.Text = row.Cells["Source"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                dtpDate.Value = DateTime.Parse(row.Cells["Date"].Value.ToString());
            }
        }
        // Utility method: Reset form inputs
        private void ClearInputs()
        {
            txtSource.Text = "";
            txtAmount.Text = "";
            dtpDate.Value = DateTime.Today;
        }
    }
    // Encapsulation: A model class to hold income data in one object
    public class IncomeEntry
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        // Constructor: Initialize income without ID (for Add)
        public IncomeEntry(string source, double amount, DateTime date)
        {
            Source = source;
            Amount = amount;
            Date = date;
        }
        // Constructor: Initialize income with ID (for Update)
        public IncomeEntry(int id, string source, double amount, DateTime date)
        {
            Id = id;
            Source = source;
            Amount = amount;
            Date = date;
        }
    }
}
