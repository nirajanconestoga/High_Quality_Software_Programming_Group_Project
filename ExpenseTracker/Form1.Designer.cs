namespace ExpenseTrackerApp
{
    partial class Form1
    {  
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvExpenses;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvExpenses = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            this.SuspendLayout();

            var defaultFont = new System.Drawing.Font("Segoe UI", 11F);

            // Labels form
            this.lblAmount.Text = "Amount:";
            this.lblAmount.Location = new System.Drawing.Point(20, 20);
            this.lblAmount.Size = new System.Drawing.Size(100, 30);
            this.lblAmount.Font = defaultFont;
            this.lblAmount.AutoSize = true;

            this.lblCategory.Text = "Category:";
            this.lblCategory.Location = new System.Drawing.Point(20, 60);
            this.lblCategory.Size = new System.Drawing.Size(100, 30);
            this.lblCategory.Font = defaultFont;
            this.lblCategory.AutoSize = true;

            this.lblDate.Text = "Date:";
            this.lblDate.Location = new System.Drawing.Point(20, 100);
            this.lblDate.Size = new System.Drawing.Size(100, 30);
            this.lblDate.Font = defaultFont;
            this.lblDate.AutoSize = true;

            this.lblDescription.Text = "Description:";
            this.lblDescription.Location = new System.Drawing.Point(20, 140);
            this.lblDescription.Size = new System.Drawing.Size(100, 30);
            this.lblDescription.Font = defaultFont;
            this.lblDescription.AutoSize = true;

            // Input Controls (moved to right)
            int inputX = 170;

            this.txtAmount.Location = new System.Drawing.Point(inputX, 20);
            this.txtAmount.Size = new System.Drawing.Size(300, 30);
            this.txtAmount.Font = defaultFont;

            this.txtCategory.Location = new System.Drawing.Point(inputX, 60);
            this.txtCategory.Size = new System.Drawing.Size(300, 30);
            this.txtCategory.Font = defaultFont;

            this.datePicker.Location = new System.Drawing.Point(inputX, 100);
            this.datePicker.Size = new System.Drawing.Size(300, 30);
            this.datePicker.Font = defaultFont;
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.txtDescription.Location = new System.Drawing.Point(inputX, 140);
            this.txtDescription.Size = new System.Drawing.Size(420, 30);
            this.txtDescription.Font = defaultFont;

            // Buttons
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(20, 190);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Font = defaultFont;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Edit";
            this.btnEdit.Location = new System.Drawing.Point(130, 190);
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.Font = defaultFont;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(240, 190);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Font = defaultFont;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // DataGridView
            this.dgvExpenses.Location = new System.Drawing.Point(20, 240);
            this.dgvExpenses.Size = new System.Drawing.Size(740, 300);
            this.dgvExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenses.ReadOnly = true;
            this.dgvExpenses.AllowUserToAddRows = false;
            this.dgvExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpenses.Font = defaultFont;

            // Form
            this.ClientSize = new System.Drawing.Size(800, 570);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvExpenses);
            this.Text = "Expense Management";
            this.Font = defaultFont;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
