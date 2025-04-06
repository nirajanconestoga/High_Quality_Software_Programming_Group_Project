using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseTracker
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnIncome;
        private Button btnExpenses;
        private Button btnBudget;
        private Button btnCategories;
        private Chart chartSummary;
        private Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();

            btnIncome = new Button();
            btnExpenses = new Button();
            btnBudget = new Button();
            btnCategories = new Button();
            chartSummary = new Chart();
            label1 = new Label();

            ((System.ComponentModel.ISupportInitialize)chartSummary).BeginInit();
            SuspendLayout();

            // btnIncome
            btnIncome.Location = new Point(30, 267);
            btnIncome.Name = "btnIncome";
            btnIncome.Size = new Size(250, 40);
            btnIncome.TabIndex = 1;
            btnIncome.Text = "Manage Income";
            btnIncome.UseVisualStyleBackColor = true;
            btnIncome.Click += btnIncome_Click;

            // btnExpenses
            btnExpenses.Location = new Point(30, 206);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.Size = new Size(250, 40);
            btnExpenses.TabIndex = 2;
            btnExpenses.Text = "Manage Expenses";
            btnExpenses.UseVisualStyleBackColor = true;
            btnExpenses.Click += btnExpenses_Click;

            // btnBudget
            btnBudget.Location = new Point(30, 146);
            btnBudget.Name = "btnBudget";
            btnBudget.Size = new Size(250, 40);
            btnBudget.TabIndex = 3;
            btnBudget.Text = "Set Budget";
            btnBudget.UseVisualStyleBackColor = true;
            btnBudget.Click += btnBudget_Click;

            // btnCategories
            btnCategories.Location = new Point(30, 327);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(250, 40);
            btnCategories.TabIndex = 4;
            btnCategories.Text = "Manage Categories";
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;

            // chartSummary
            chartArea1.Name = "ChartArea1";
            chartSummary.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartSummary.Legends.Add(legend1);
            chartSummary.Location = new Point(316, 115);
            chartSummary.Name = "chartSummary";
            chartSummary.Size = new Size(370, 350);
            chartSummary.TabIndex = 5;
            chartSummary.Text = "Income Chart";

            // label1
            label1.AutoSize = true;
            label1.Location = new Point(155, 47);
            label1.Name = "label1";
            label1.Size = new Size(300, 20);
            label1.TabIndex = 6;
            label1.Text = "Welcome to the Expense Tracker Dashboard";
            label1.Click += label1_Click;

            // DashboardForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(725, 500);
            Controls.Add(label1);
            Controls.Add(btnIncome);
            Controls.Add(btnExpenses);
            Controls.Add(btnBudget);
            Controls.Add(btnCategories);
            Controls.Add(chartSummary);
            Name = "DashboardForm";
            Text = "Dashboard - Expense Tracker";
            ((System.ComponentModel.ISupportInitialize)chartSummary).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
