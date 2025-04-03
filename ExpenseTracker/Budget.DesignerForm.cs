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
            lblMonth.Location = new Point(34, 54);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(65, 25);
            lblMonth.TabIndex = 0;
            lblMonth.Text = "Month";
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(34, 108);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(44, 25);
            lblYear.TabIndex = 1;
            lblYear.Text = "Year";
            // 
            // lblLimit
            // 
            lblLimit.AutoSize = true;
            lblLimit.Location = new Point(34, 173);
            lblLimit.Name = "lblLimit";
            lblLimit.Size = new Size(121, 25);
            lblLimit.TabIndex = 2;
            lblLimit.Text = "Monthly Limit";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(81, 326);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(59, 25);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "label4";
            // 
            // cmbMonth
            // 
            cmbMonth.FormattingEnabled = true;
            cmbMonth.Location = new Point(177, 63);
            cmbMonth.Name = "cmbMonth";
            cmbMonth.Size = new Size(182, 33);
            cmbMonth.TabIndex = 4;
            // 
            // txtLimit
            // 
            txtLimit.Location = new Point(175, 170);
            txtLimit.Name = "txtLimit";
            txtLimit.Size = new Size(150, 31);
            txtLimit.TabIndex = 5;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(175, 108);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(150, 31);
            txtYear.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(156, 252);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(184, 34);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save Budget";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(txtYear);
            Controls.Add(txtLimit);
            Controls.Add(cmbMonth);
            Controls.Add(lblStatus);
            Controls.Add(lblLimit);
            Controls.Add(lblYear);
            Controls.Add(lblMonth);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
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
