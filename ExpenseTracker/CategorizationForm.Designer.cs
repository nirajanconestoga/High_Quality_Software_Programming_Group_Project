namespace Expense_Tracker
{
    partial class CategorizationForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCategory;
        private TextBox txtCategoryInput;
        private Button btnAddCategory;
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
            dgvCategoryList = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).BeginInit();
            SuspendLayout();

            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 11F);
            lblCategory.Location = new Point(20, 20);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(150, 25);
            lblCategory.Text = "New Category Name:";

            // txtCategoryInput
            txtCategoryInput.Font = new Font("Segoe UI", 11F);
            txtCategoryInput.Location = new Point(200, 18);
            txtCategoryInput.Size = new Size(250, 32);

            // btnAddCategory
            btnAddCategory.Font = new Font("Segoe UI", 11F);
            btnAddCategory.Location = new Point(470, 17);
            btnAddCategory.Size = new Size(120, 35);
            btnAddCategory.Text = "Add";
            btnAddCategory.Click += btnAddCategory_Click;

            // dgvCategoryList
            dgvCategoryList.AllowUserToAddRows = false;
            dgvCategoryList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategoryList.Location = new Point(20, 70);
            dgvCategoryList.Size = new Size(570, 300);
            dgvCategoryList.ReadOnly = true;
            dgvCategoryList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // CategorizationForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 400);
            Controls.Add(lblCategory);
            Controls.Add(txtCategoryInput);
            Controls.Add(btnAddCategory);
            Controls.Add(dgvCategoryList);
            Name = "CategorizationForm";
            Text = "Manage Categories";
            StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
