using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ExpenseTracker.Database;
using ExpenseTrackerApp;         // For expenseForm
using Expense_Tracker;           // For Budget & CategorizationForm

namespace ExpenseTracker
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            Load += DashboardForm_Load; // Hook up form load event
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // ✅ Initialize all relevant DBs
            income_managementdb.InitializeDatabase();
            Budgetdb.InitializeDatabase();
            expenseForm.InitializeDatabase(); // ✅ Add this to avoid "no such table" errors

            LoadChart(); // Load summary chart
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            var incomeForm = new Income_ManagementForm();
            incomeForm.IncomeSaved += LoadChart; // 🔁 Refresh chart on save
            incomeForm.ShowDialog();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            var expForm = new expenseForm();
            expForm.ExpenseSaved += LoadChart; // 🔁 Refresh chart on save
            expForm.ShowDialog();
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            var budgetForm = new Budget();
            budgetForm.BudgetSaved += (month, year, limit) => LoadChart(); // 🔁 Refresh chart on save
            budgetForm.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            var catForm = new CategorizationForm();
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
                decimal remaining = Math.Max(0, budget - expenses);

                series.Points.AddXY("Income", income);
                series.Points.AddXY("Expenses", expenses);
                series.Points.AddXY("Remaining Budget", remaining);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message);
            }

            // ✅ Chart visual settings
            if (chartSummary.ChartAreas.Count > 0)
                chartSummary.ChartAreas[0].Area3DStyle.Enable3D = true;

            if (chartSummary.Legends.Count > 0)
                chartSummary.Legends[0].Docking = Docking.Bottom;

            chartSummary.Series.Add(series);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: interactivity
        }
    }
}
