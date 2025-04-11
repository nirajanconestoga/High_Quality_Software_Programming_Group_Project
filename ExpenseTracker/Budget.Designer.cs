namespace ExpenseTracker

{
    partial class Budget
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
            lblMonth = new Label();
            lblYear = new Label();
            lblLimit = new Label();
            lblStatus = new Label();
            cmbMonth = new ComboBox();
            txtLimit = new TextBox();
            txtYear = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // lblMonth
            // 
            lblMonth.AutoSize = true;
            lblMonth.Location = new Point(27, 43);
            lblMonth.Margin = new Padding(2, 0, 2, 0);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(52, 20);
            lblMonth.TabIndex = 0;
            lblMonth.Text = "Month";
            lblMonth.Click += lblMonth_Click;
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(27, 86);
            lblYear.Margin = new Padding(2, 0, 2, 0);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(37, 20);
            lblYear.TabIndex = 1;
            lblYear.Text = "Year";
            // 
            // lblLimit
            // 
            lblLimit.AutoSize = true;
            lblLimit.Location = new Point(27, 138);
            lblLimit.Margin = new Padding(2, 0, 2, 0);
            lblLimit.Name = "lblLimit";
            lblLimit.Size = new Size(100, 20);
            lblLimit.TabIndex = 2;
            lblLimit.Text = "Monthly Limit";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(142, 256);
            lblStatus.Margin = new Padding(2, 0, 2, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(136, 20);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "No Budget Entered";
            lblStatus.Click += lblStatus_Click;
            // 
            // cmbMonth
            // 
            cmbMonth.FormattingEnabled = true;
            cmbMonth.Location = new Point(142, 35);
            cmbMonth.Margin = new Padding(2);
            cmbMonth.Name = "cmbMonth";
            cmbMonth.Size = new Size(146, 28);
            cmbMonth.TabIndex = 4;
            cmbMonth.SelectedIndexChanged += cmbMonth_SelectedIndexChanged;
            // 
            // txtLimit
            // 
            txtLimit.Location = new Point(142, 131);
            txtLimit.Margin = new Padding(2);
            txtLimit.Name = "txtLimit";
            txtLimit.Size = new Size(121, 27);
            txtLimit.TabIndex = 5;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(142, 79);
            txtYear.Margin = new Padding(2);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(121, 27);
            txtYear.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(142, 193);
            btnSave.Margin = new Padding(2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(147, 27);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save Budget";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Budget
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 360);
            Controls.Add(btnSave);
            Controls.Add(txtYear);
            Controls.Add(txtLimit);
            Controls.Add(cmbMonth);
            Controls.Add(lblStatus);
            Controls.Add(lblLimit);
            Controls.Add(lblYear);
            Controls.Add(lblMonth);
            Margin = new Padding(2);
            Name = "Budget";
            Text = "No Budget Saved";
            Load += Budget_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMonth;
        private Label lblYear;
        private Label lblLimit;
        private Label lblStatus;
        private ComboBox cmbMonth;
        private TextBox txtLimit;
        private TextBox txtYear;
        private Button btnSave;
    }
}
