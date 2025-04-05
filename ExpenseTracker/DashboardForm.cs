using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ExpenseTracker.Database;
using Expense_Tracker; // Namespace for CategorizationForm and Budget

namespace ExpenseTracker
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income_ManagementForm incomeForm = new Income_ManagementForm();
            incomeForm.ShowDialog();
            LoadChart(); // Refresh chart after adding income
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Expense management module is under development.");
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            // Open the Budget form when the Manage Budget button is clicked
            Budget budgetForm = new Budget(); // Assuming Budget is the class for the Budget form
            budgetForm.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            // Open CategorizationForm when the button is clicked
            CategorizationForm catForm = new CategorizationForm();
            catForm.ShowDialog();
        }

        private void LoadChart()
        {
            chartSummary.Series.Clear();
            chartSummary.Titles.Clear();
            chartSummary.Titles.Add("Income by Source");

            var incomeSeries = new Series("Income")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            try
            {
                using (var conn = income_managementdb.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT Source, SUM(Amount) AS Total FROM Income GROUP BY Source";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string source = reader["Source"].ToString();
                            double total = Convert.ToDouble(reader["Total"]);
                            incomeSeries.Points.AddXY(source, total);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message);
            }

            chartSummary.Series.Add(incomeSeries);
        }
    }
}
