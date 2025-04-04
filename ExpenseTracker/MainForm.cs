using System;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "📊 Welcome to Expense Tracker Dashboard";
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income_ManagementForm incomeForm = new Income_ManagementForm();
            incomeForm.ShowDialog();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Expense management module is under development.");
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Budget module is coming soon.");
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Category module is coming soon.");
        }
    }
}
