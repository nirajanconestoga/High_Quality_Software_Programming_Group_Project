using ExpenseTracker.Database;
using System;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class Income_ManagementForm : Form
    {
        // 🔁 Event to notify Dashboard
        public delegate void IncomeSavedHandler();
        public event IncomeSavedHandler IncomeSaved;

        public Income_ManagementForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            income_managementdb.InitializeDatabase();
            LoadIncomeData();
        }

        private void LoadIncomeData()
        {
            dgvIncome.DataSource = income_managementdb.GetIncomeList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var income = new IncomeEntry(
                    txtSource.Text,
                    double.Parse(txtAmount.Text),
                    dtpDate.Value
                );

                income_managementdb.AddIncome(income.Source, income.Amount, income.Date);
                LoadIncomeData();
                ClearInputs();
                IncomeSaved?.Invoke(); // 🔁 Notify Dashboard
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
                    IncomeSaved?.Invoke(); // 🔁 Notify Dashboard
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

        private void ClearInputs()
        {
            txtSource.Text = "";
            txtAmount.Text = "";
            dtpDate.Value = DateTime.Today;
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            // Optional text changed logic
        }
    }

    public class IncomeEntry
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public IncomeEntry(string source, double amount, DateTime date)
        {
            Source = source;
            Amount = amount;
            Date = date;
        }

        public IncomeEntry(int id, string source, double amount, DateTime date)
        {
            Id = id;
            Source = source;
            Amount = amount;
            Date = date;
        }
    }
}
