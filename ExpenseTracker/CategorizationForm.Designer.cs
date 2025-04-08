namespace Expense_Tracker
{
    partial class CategorizationForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCategory;
        private TextBox txtCategoryInput;
        private Button btnAddCategory;
        private Button btnUpdateCategory;
        private Button btnDeleteCategory;
        private DataGridView dgvCategoryList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblCategory = new Label();
            txtCategoryInput = new TextBox();
            btnAddCategory = new Button();
            btnUpdateCategory = new Button();
            btnDeleteCategory = new Button();
            dgvCategoryList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).BeginInit();
            SuspendLayout();

            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 11F);
            lblCategory.Location = new Point(20, 20);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(190, 25);
            lblCategory.TabIndex = 0;
            lblCategory.Text = "New Category Name:";
            lblCategory.Click += lblCategory_Click;

            // txtCategoryInput
            txtCategoryInput.Font = new Font("Segoe UI", 11F);
            txtCategoryInput.Location = new Point(220, 18);
            txtCategoryInput.Name = "txtCategoryInput";
            txtCategoryInput.Size = new Size(220, 32);
            txtCategoryInput.TabIndex = 1;

            // btnAddCategory
            btnAddCategory.Font = new Font("Segoe UI", 11F);
            btnAddCategory.Location = new Point(460, 17);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(120, 35);
            btnAddCategory.TabIndex = 2;
            btnAddCategory.Text = "Add";
            btnAddCategory.UseVisualStyleBackColor = true;
            btnAddCategory.Click += btnAddCategory_Click;

            // btnUpdateCategory
            btnUpdateCategory.Font = new Font("Segoe UI", 11F);
            btnUpdateCategory.Location = new Point(460, 60);
            btnUpdateCategory.Name = "btnUpdateCategory";
            btnUpdateCategory.Size = new Size(120, 35);
            btnUpdateCategory.TabIndex = 3;
            btnUpdateCategory.Text = "Update";
            btnUpdateCategory.UseVisualStyleBackColor = true;
            btnUpdateCategory.Click += btnUpdateCategory_Click;

            // btnDeleteCategory
            btnDeleteCategory.Font = new Font("Segoe UI", 11F);
            btnDeleteCategory.Location = new Point(460, 105);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(120, 35);
            btnDeleteCategory.TabIndex = 4;
            btnDeleteCategory.Text = "Delete";
            btnDeleteCategory.UseVisualStyleBackColor = true;
            btnDeleteCategory.Click += btnDeleteCategory_Click;

            // dgvCategoryList
            dgvCategoryList.AllowUserToAddRows = false;
            dgvCategoryList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategoryList.ColumnHeadersHeight = 29;
            dgvCategoryList.Location = new Point(20, 160);
            dgvCategoryList.Name = "dgvCategoryList";
            dgvCategoryList.ReadOnly = true;
            dgvCategoryList.RowHeadersWidth = 51;
            dgvCategoryList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategoryList.Size = new Size(560, 300);
            dgvCategoryList.TabIndex = 5;
            dgvCategoryList.CellClick += dgvCategoryList_CellClick;

            // CategorizationForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 480);
            Controls.Add(lblCategory);
            Controls.Add(txtCategoryInput);
            Controls.Add(btnAddCategory);
            Controls.Add(btnUpdateCategory);
            Controls.Add(btnDeleteCategory);
            Controls.Add(dgvCategoryList);
            Name = "CategorizationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Categories";
            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
