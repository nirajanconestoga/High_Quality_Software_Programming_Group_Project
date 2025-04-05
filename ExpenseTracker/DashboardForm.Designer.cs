using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblWelcome;
        private Button btnIncome;
        private Button btnExpenses;
        private Button btnBudget;
        private Button btnCategories;
        private Chart chartSummary;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            btnIncome = new Button();
            btnExpenses = new Button();
            btnBudget = new Button();
            btnCategories = new Button();
            chartSummary = new Chart();
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();

            SuspendLayout();

            // lblWelcome
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcome.Location = new Point(30, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(381, 32);
            lblWelcome.Text = "📊 Welcome to Expense Tracker";

            // btnIncome
            btnIncome.Location = new Point(30, 70);
            btnIncome.Name = "btnIncome";
            btnIncome.Size = new Size(250, 40);
            btnIncome.Text = "Manage Income";
            btnIncome.UseVisualStyleBackColor = true;
            btnIncome.Click += btnIncome_Click;

            // btnExpenses
            btnExpenses.Location = new Point(30, 120);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.Size = new Size(250, 40);
            btnExpenses.Text = "Manage Expenses";
            btnExpenses.UseVisualStyleBackColor = true;
            btnExpenses.Click += btnExpenses_Click;

            // btnBudget
            btnBudget.Location = new Point(30, 170);
            btnBudget.Name = "btnBudget";
            btnBudget.Size = new Size(250, 40);
            btnBudget.Text = "Set Budget";
            btnBudget.UseVisualStyleBackColor = true;
            btnBudget.Click += btnBudget_Click;

            // btnCategories
            btnCategories.Location = new Point(30, 220);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(250, 40);
            btnCategories.Text = "Manage Categories";
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;

            // chartSummary
            chartArea1.Name = "ChartArea1";
            chartSummary.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartSummary.Legends.Add(legend1);
            chartSummary.Location = new Point(320, 70);
            chartSummary.Name = "chartSummary";
            chartSummary.Size = new Size(370, 350);
            chartSummary.Text = "Income Chart";

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(725, 500);
            Controls.Add(lblWelcome);
            Controls.Add(btnIncome);
            Controls.Add(btnExpenses);
            Controls.Add(btnBudget);
            Controls.Add(btnCategories);
            Controls.Add(chartSummary);
            Name = "MainForm";
            Text = "Dashboard - Expense Tracker";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
