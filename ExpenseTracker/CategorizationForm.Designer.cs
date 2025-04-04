namespace Expense_Tracker
{
    partial class CategorizationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblCategory = new Label();
            txtCategoryInput = new TextBox();
            btnAddCategory = new Button();
            dgvCategoryList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).BeginInit();
            SuspendLayout();
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(80, 39);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(107, 20);
            lblCategory.TabIndex = 0;
            lblCategory.Text = "Enter Category";
            // 
            // txtCategoryInput
            // 
            txtCategoryInput.Location = new Point(202, 36);
            txtCategoryInput.Name = "txtCategoryInput";
            txtCategoryInput.Size = new Size(125, 27);
            txtCategoryInput.TabIndex = 1;
            // 
            // btnAddCategory
            // 
            btnAddCategory.Location = new Point(373, 39);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(168, 29);
            btnAddCategory.TabIndex = 2;
            btnAddCategory.Text = "Add Category";
            btnAddCategory.UseVisualStyleBackColor = true;
            btnAddCategory.Click += btnAddCategory_Click;
            // 
            // dgvCategoryList
            // 
            dgvCategoryList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategoryList.Location = new Point(80, 114);
            dgvCategoryList.Name = "dgvCategoryList";
            dgvCategoryList.RowHeadersWidth = 51;
            dgvCategoryList.Size = new Size(300, 188);
            dgvCategoryList.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvCategoryList);
            Controls.Add(btnAddCategory);
            Controls.Add(txtCategoryInput);
            Controls.Add(lblCategory);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategoryList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategory;
        private TextBox txtCategoryInput;
        private Button btnAddCategory;
        private DataGridView dgvCategoryList;
    }
}
