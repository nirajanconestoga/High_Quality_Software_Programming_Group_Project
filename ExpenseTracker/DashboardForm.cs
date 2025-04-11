using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Concept: Charting library for visualization
using ExpenseTracker.Database;      // Concept: Namespaces for modular access to DB classes
using ExpenseTrackerApp;           // Used for accessing expenseForm
using Expense_Tracker;             // Used for Budget and CategorizationForm

namespace ExpenseTracker
{
    public partial class DashboardForm : Form
    {
        // Constructor - Form setup
        public DashboardForm()
        {
            InitializeComponent();                   // Concept: UI Initialization
            Load += DashboardForm_Load;              // Concept: Event Handling (Form Load)
        }

        // Form Load Event - initializes DB and loads chart
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Concept: Initialization of database tables to prevent runtime errors
            income_managementdb.InitializeDatabase();
            Budgetdb.InitializeDatabase();
            expenseForm.InitializeDatabase();

            LoadChart(); // Load chart to display financial summary
        }

        // Open Income Management form
        private void btnIncome_Click(object sender, EventArgs e)
        {
            var incomeForm = new Income_ManagementForm();

            // Concept: Delegates & Events - subscribe to IncomeSaved to auto-refresh chart
            incomeForm.IncomeSaved += LoadChart;

            incomeForm.ShowDialog(); // Concept: Modal dialog box to focus on income entry
        }

        // Open Expense Form
        private void btnExpenses_Click(object sender, EventArgs e)
        {
            var expForm = new expenseForm();

            // Subscribe to event to refresh chart when expenses change
            expForm.ExpenseSaved += LoadChart;

            expForm.ShowDialog(); // Open expense form modally
        }

        // Open Budget Form
        private void btnBudget_Click(object sender, EventArgs e)
        {
            var budgetForm = new Budget();

            // Lambda expression as event handler - used to refresh chart
            budgetForm.BudgetSaved += (month, year, limit) => LoadChart();

            budgetForm.ShowDialog();
        }

        // Open Categorization Form
        private void btnCategories_Click(object sender, EventArgs e)
        {
            var catForm = new CategorizationForm();
            catForm.ShowDialog(); // Simply opens the form; no event subscription
        }

        // Load Chart with summary of income, expenses, and budget
        private void LoadChart()
        {
            // Concept: Data visualization using System.Windows.Forms.DataVisualization.Charting
            chartSummary.Series.Clear();       // Clear previous data
            chartSummary.Titles.Clear();       // Clear existing titles
            chartSummary.Titles.Add("Monthly Financial Overview"); // Add chart title

            var series = new Series("Summary")
            {
                ChartType = SeriesChartType.Pie,         // Concept: Pie Chart type
                IsValueShownAsLabel = true               // Show data labels
            };

            try
            {
                // Concept: DateTime formatting to match DB month format
                string fullMonthName = DateTime.Now.ToString("MMMM");   // e.g., April
                string numericMonth = DateTime.Now.ToString("MM");      // e.g., 04
                string year = DateTime.Now.ToString("yyyy");            // e.g., 2025

                // Concept: Database calls to get summary values
                decimal income = income_managementdb.GetTotalIncomeForMonth(numericMonth, year);
                decimal expenses = expenseForm.GetTotalExpensesForMonth(numericMonth, year);
                decimal budget = Budgetdb.GetBudgetForMonth(fullMonthName, year);
                decimal remaining = Math.Max(0, budget - expenses); // Prevent negative values

                // Add chart points
                series.Points.AddXY("Income", income);
                series.Points.AddXY("Expenses", expenses);
                series.Points.AddXY("Remaining Budget", remaining);
            }
            catch (Exception ex)
            {
                // Concept: Exception handling for robustness
                MessageBox.Show("Error loading chart: " + ex.Message);
            }

            // Concept: Customizing chart visual appearance
            if (chartSummary.ChartAreas.Count > 0)
                chartSummary.ChartAreas[0].Area3DStyle.Enable3D = true; // Enable 3D effect

            if (chartSummary.Legends.Count > 0)
                chartSummary.Legends[0].Docking = Docking.Bottom;       // Place legend at bottom

            chartSummary.Series.Add(series); // Add data series to the chart
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: Reserved for label interactivity (e.g., navigation/help)
        }
    }
}
