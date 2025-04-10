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
    // Partial class extending Form (Windows Forms)
    // Partial class concept: Allows splitting class definition across multiple files
    public partial class LoginForm : Form
    {
        // Constructor
        // Form lifecycle concept: Initializes form components
        public LoginForm()
        {
            InitializeComponent();  // Auto-generated method to initialize UI components
        }

        // Login button click event handler
        // Event-driven programming concept: Responds to user interaction
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Input validation
            // Defensive programming concept: Check for empty/null values
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and password are required!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Early exit pattern
            }

            try  // Exception handling concept: Graceful error management
            {
                // Database connection pattern: Using ensures proper disposal
                using (var connection = Database.Login_Registerdb.GetConnection())
                {
                    connection.Open();  // Connection must be explicitly opened

                    // Parameterized SQL query
                    // SQL injection prevention: Using parameters instead of string concatenation
                    string selectUser = "SELECT PasswordHash, Salt FROM Users WHERE Username = @username";

                    using (var command = new SQLiteCommand(selectUser, connection))
                    {
                        // Parameter binding
                        command.Parameters.AddWithValue("@username", txtUsername.Text);

                        // Data reader pattern: Forward-only, read-only result access
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // If user exists
                            {
                                // Retrieve stored credentials
                                string storedHash = reader["PasswordHash"].ToString();
                                string salt = reader["Salt"].ToString();

                                // Password verification concept:
                                // Hash entered password with stored salt for comparison
                                string enteredHash = PasswordHelper.HashPassword(txtPassword.Text, salt);

                                // Secure comparison
                                if (storedHash == enteredHash)
                                {
                                    // Authentication success
                                    // DialogResult concept: Communicates result to calling code
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();  // Close form on success
                                }
                                else
                                {
                                    // Generic error message (security best practice)
                                    MessageBox.Show("Invalid username or password!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                // User not found (same message for security)
                                MessageBox.Show("Invalid username or password!", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)  // Catch-all for database/other errors
            {
                // Error reporting to user
                MessageBox.Show("Login failed: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cancel button click event handler
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Set dialog result and close
            this.DialogResult = DialogResult.Cancel;
            this.Close();  // Form closing concept
        }
    }
}
