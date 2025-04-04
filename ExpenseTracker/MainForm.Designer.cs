namespace ExpenseTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Declare all controls here
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnIncome;
        private System.Windows.Forms.Button btnExpenses;
        private System.Windows.Forms.Button btnBudget;
        private System.Windows.Forms.Button btnCategories;

        
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnIncome = new System.Windows.Forms.Button();
            this.btnExpenses = new System.Windows.Forms.Button();
            this.btnBudget = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.SuspendLayout();
           
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(350, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "📊 Welcome to Expense Tracker";
         
            // btnIncome
            this.btnIncome.Location = new System.Drawing.Point(30, 70);
            this.btnIncome.Name = "btnIncome";
            this.btnIncome.Size = new System.Drawing.Size(250, 40);
            this.btnIncome.TabIndex = 1;
            this.btnIncome.Text = "Manage Income";
            this.btnIncome.UseVisualStyleBackColor = true;
            this.btnIncome.Click += new System.EventHandler(this.btnIncome_Click);
            // btnExpenses 
            this.btnExpenses.Location = new System.Drawing.Point(30, 120);
            this.btnExpenses.Name = "btnExpenses";
            this.btnExpenses.Size = new System.Drawing.Size(250, 40);
            this.btnExpenses.TabIndex = 2;
            this.btnExpenses.Text = "Manage Expenses";
            this.btnExpenses.UseVisualStyleBackColor = true;
            this.btnExpenses.Click += new System.EventHandler(this.btnExpenses_Click);
       
            // btnBudget
            this.btnBudget.Location = new System.Drawing.Point(30, 170);
            this.btnBudget.Name = "btnBudget";
            this.btnBudget.Size = new System.Drawing.Size(250, 40);
            this.btnBudget.TabIndex = 3;
            this.btnBudget.Text = "Set Budget";
            this.btnBudget.UseVisualStyleBackColor = true;
            this.btnBudget.Click += new System.EventHandler(this.btnBudget_Click);

            // btnCategories
            this.btnCategories.Location = new System.Drawing.Point(30, 220);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(250, 40);
            this.btnCategories.TabIndex = 4;
            this.btnCategories.Text = "Manage Categories";
            this.btnCategories.UseVisualStyleBackColor = true;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 300);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnIncome);
            this.Controls.Add(this.btnExpenses);
            this.Controls.Add(this.btnBudget);
            this.Controls.Add(this.btnCategories);
            this.Name = "MainForm";
            this.Text = "Dashboard - Expense Tracker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
