using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ExpenseTracker
{
    
        // Partial class for user registration form
        // Windows Forms inheritance: Inherits from Form base class
        public partial class RegisterForm : Form
        {
            // Constructor
            // Form initialization concept: Sets up form properties
            public RegisterForm()
            {
                InitializeComponent();  // Designer-generated initialization

                // Form configuration
                this.Text = "User Registration";  // Window title
                this.StartPosition = FormStartPosition.CenterParent;  // Center on parent window
                this.FormBorderStyle = FormBorderStyle.FixedDialog;  // Fixed-size dialog
                this.MaximizeBox = false;  // Disable maximize button
                this.MinimizeBox = false;  // Disable minimize button

                // Additional control initialization
                InitializeControls();
            }

            // Method to configure controls programmatically
            // UI configuration concept: Alternative to designer setup
            private void InitializeControls()
            {
                // Label setup
                lblUsername.Text = "Username:";
                lblPassword.Text = "Password:";
                lblConfirmPassword.Text = "Confirm Password:";

                // Password field configuration
                txtPassword.PasswordChar = '*';  // Mask password input
                txtConfirmPassword.PasswordChar = '*';

                // Button setup
                btnRegister.Text = "Register";
                btnCancel.Text = "Cancel";

                // Tab order configuration
                // UI navigation concept: Controls tab sequence
                txtUsername.TabIndex = 0;
                txtPassword.TabIndex = 1;
                txtConfirmPassword.TabIndex = 2;
                btnRegister.TabIndex = 3;
                btnCancel.TabIndex = 4;
            }

            // Register button click handler
            // Event handling concept: Responds to user action
            private void btnRegister_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.None;  // Reset dialog result

                // Input validation
                // Data validation concept: Ensure valid input before processing
                if (!ValidateInputs())
                    return;

                try
                {
                    // Registration attempt
                    if (RegisterNewUser())
                    {
                        // Success notification
                        MessageBox.Show("Registration successful! You can now login.",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;  // Signal success
                        this.Close();  // Close the form
                    }
                }
                catch (SQLiteException ex)
                {
                    // Database-specific error handling
                    HandleDatabaseError(ex);
                }
                catch (Exception ex)
                {
                    // General error handling
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }

            private bool ValidateInputs()
        {
            // Check for empty fields
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Check password match
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return false;
            }

            // Check password strength (optional)
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                txtPassword.SelectAll();
                return false;
            }

            return true;
        }

        // User registration method
        // Database operation concept: CRUD operation (Create)
        private bool RegisterNewUser()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Password security concept: Hashing with salt
            string salt = PasswordHelper.GenerateSalt();
            string passwordHash = PasswordHelper.HashPassword(password, salt);

            using (var connection = Database.Login_Registerdb.GetConnection())
            {
                connection.Open();

                // Check for existing username
                // Database query concept: Scalar query
                using (var checkCmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM Users WHERE Username = @username", connection))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Username already exists! Please choose a different one.",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // Insert new user record
                // Parameterized query concept: Safe SQL execution
                using (var insertCmd = new SQLiteCommand(
                    "INSERT INTO Users (Username, PasswordHash, Salt) VALUES (@username, @passwordHash, @salt)",
                    connection))
                {
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    insertCmd.Parameters.AddWithValue("@salt", salt);

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    return rowsAffected > 0;  // Return true if insertion succeeded
                }
            }
        }

        // Database error handler
        // Error handling specialization: SQLite-specific cases
        private void HandleDatabaseError(SQLiteException ex)
        {
            if (ex.Message.Contains("UNIQUE constraint failed"))
            {
                MessageBox.Show("Username already exists! Please choose a different one.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show($"Database error: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}



