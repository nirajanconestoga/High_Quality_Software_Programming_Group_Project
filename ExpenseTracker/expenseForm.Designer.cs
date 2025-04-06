namespace ExpenseTrackerApp
{
    partial class expenseForm
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
            lblAmount = new Label();
            lblCategory = new Label();
            lblDate = new Label();
            lblDescription = new Label();
            txtAmount = new TextBox();
            txtCategory = new TextBox();
            datePicker = new DateTimePicker();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            dgvExpenses = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            SuspendLayout();
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Segoe UI", 11F);
            lblAmount.Location = new Point(20, 20);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(83, 25);
            lblAmount.TabIndex = 0;
            lblAmount.Text = "Amount:";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 11F);
            lblCategory.Location = new Point(20, 60);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(92, 25);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Category:";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 11F);
            lblDate.Location = new Point(20, 100);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(55, 25);
            lblDate.TabIndex = 2;
            lblDate.Text = "Date:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 11F);
            lblDescription.Location = new Point(20, 140);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(112, 25);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Description:";
            // 
            // txtAmount
            // 
            txtAmount.Font = new Font("Segoe UI", 11F);
            txtAmount.Location = new Point(170, 20);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(300, 32);
            txtAmount.TabIndex = 4;
            txtAmount.TextChanged += txtAmount_TextChanged;
            // 
            // txtCategory
            // 
            txtCategory.Font = new Font("Segoe UI", 11F);
            txtCategory.Location = new Point(170, 60);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(300, 32);
            txtCategory.TabIndex = 5;
            // 
            // datePicker
            // 
            datePicker.Font = new Font("Segoe UI", 11F);
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Location = new Point(170, 100);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(300, 32);
            datePicker.TabIndex = 6;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 11F);
            txtDescription.Location = new Point(170, 140);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(420, 32);
            txtDescription.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 11F);
            btnAdd.Location = new Point(20, 190);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Segoe UI", 11F);
            btnEdit.Location = new Point(130, 190);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 35);
            btnEdit.TabIndex = 9;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 11F);
            btnDelete.Location = new Point(240, 190);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvExpenses
            // 
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpenses.ColumnHeadersHeight = 29;
            dgvExpenses.Font = new Font("Segoe UI", 11F);
            dgvExpenses.Location = new Point(20, 240);
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.ReadOnly = true;
            dgvExpenses.RowHeadersWidth = 51;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.Size = new Size(740, 300);
            dgvExpenses.TabIndex = 11;
            // 
            // Form1
            // 
            ClientSize = new Size(800, 570);
            Controls.Add(lblAmount);
            Controls.Add(lblCategory);
            Controls.Add(lblDate);
            Controls.Add(lblDescription);
            Controls.Add(txtAmount);
            Controls.Add(txtCategory);
            Controls.Add(datePicker);
            Controls.Add(txtDescription);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(dgvExpenses);
            Font = new Font("Segoe UI", 11F);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expense Management";
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
