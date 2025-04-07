namespace ExpenseTracker
{
    partial class Income_ManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtSource = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtAmount = new TextBox();
            dtpDate = new DateTimePicker();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dgvIncome = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvIncome).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(143, 20);
            label1.TabIndex = 0;
            label1.Text = "Enter income source\n";
            // 
            // txtSource
            // 
            txtSource.Location = new Point(12, 50);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(125, 27);
            txtSource.TabIndex = 1;
            txtSource.TextChanged += txtSource_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 103);
            label2.Name = "label2";
            label2.Size = new Size(98, 20);
            label2.TabIndex = 2;
            label2.Text = "Enter amount\n";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(170, 103);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 3;
            label3.Text = "Select date\n";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(12, 144);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(125, 27);
            txtAmount.TabIndex = 5;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(170, 142);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(239, 27);
            dtpDate.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(440, 142);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add income\n";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(160, 386);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(134, 29);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update selected\n";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(170, 340);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(124, 29);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete selected\n";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvIncome
            // 
            dgvIncome.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIncome.Location = new Point(310, 184);
            dgvIncome.Name = "dgvIncome";
            dgvIncome.RowHeadersWidth = 51;
            dgvIncome.Size = new Size(478, 264);
            dgvIncome.TabIndex = 10;
            dgvIncome.CellContentClick += dgvIncome_CellContentClick;
            // 
            // Income_ManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvIncome);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dtpDate);
            Controls.Add(txtAmount);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtSource);
            Controls.Add(label1);
            Name = "Income_ManagementForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvIncome).EndInit();
            this.Load += new System.EventHandler(this.Form1_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;   
        private TextBox txtSource;
        private Label label2;
        private Label label3;
        private TextBox txtAmount;
        private DateTimePicker dtpDate;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private DataGridView dgvIncome;
    }
}