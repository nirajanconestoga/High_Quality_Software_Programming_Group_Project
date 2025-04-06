using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ExpenseTracker.Database;
using ExpenseTrackerApp;
using Expense_Tracker;

namespace ExpenseTracker
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            Load += DashboardForm_Load; // Hook up the form load event
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income_ManagementForm incomeForm = new Income_ManagementForm();
            incomeForm.IncomeSaved += LoadChart;
            incomeForm.ShowDialog();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            expenseForm expForm = new expenseForm();
            expForm.ExpenseSaved += LoadChart;
            expForm.ShowDialog();
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            Budget budgetForm = new Budget();
            budgetForm.BudgetSaved += (month, year, limit) => LoadChart(); // ✅ Hook event
            budgetForm.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            CategorizationForm catForm = new CategorizationForm();
            catForm.ShowDialog();
        }

        private void LoadChart()
        {
            chartSummary.Series.Clear();
            chartSummary.Titles.Clear();
            chartSummary.Titles.Add("Monthly Financial Overview");

            var series = new Series("Summary")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            try
            {
                string month = DateTime.Now.ToString("MM");
                string year = DateTime.Now.ToString("yyyy");

                decimal income = income_managementdb.GetTotalIncomeForMonth(month, year);
                decimal expenses = expenseForm.GetTotalExpensesForMonth(month, year);
                decimal budget = Budgetdb.GetBudgetForMonth(month, year);
                decimal remaining = budget - expenses;

                series.Points.AddXY("Income", income);
                series.Points.AddXY("Expenses", expenses);
                series.Points.AddXY("Remaining", remaining > 0 ? remaining : 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message);
            }

            chartSummary.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartSummary.Legends[0].Docking = Docking.Bottom;
            chartSummary.Series.Add(series);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: add interactivity or info
        }
    }
}
