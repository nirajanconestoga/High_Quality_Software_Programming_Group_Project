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
    public partial class RegisterForm : Form
 {
     public RegisterForm()
     {
         InitializeComponent();

         // Set form properties
         this.Text = "User Registration";
         this.StartPosition = FormStartPosition.CenterParent;
         this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;

         // Initialize controls (if not done in designer)
         InitializeControls();
     }

     private void InitializeControls()
     {
         // Set up controls programmatically (optional - can be done in designer)
         lblUsername.Text = "Username:";
         lblPassword.Text = "Password:";
         lblConfirmPassword.Text = "Confirm Password:";

         txtPassword.PasswordChar = '*';
         txtConfirmPassword.PasswordChar = '*';

         btnRegister.Text = "Register";
         btnCancel.Text = "Cancel";

         // Set tab order
         txtUsername.TabIndex = 0;
         txtPassword.TabIndex = 1;
         txtConfirmPassword.TabIndex = 2;
         btnRegister.TabIndex = 3;
         btnCancel.TabIndex = 4;
     }

     private void btnRegister_Click(object sender, EventArgs e)
     {
         // Reset dialog result
         this.DialogResult = DialogResult.None;

         // Validate inputs
         if (!ValidateInputs())
             return;

         try
         {
             // Attempt registration
             if (RegisterNewUser())
             {
                 MessageBox.Show("Registration successful! You can now login.",
                               "Success",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
                 this.DialogResult = DialogResult.OK;
                 this.Close();
             }
         }
         catch (SQLiteException ex)
         {
             HandleDatabaseError(ex);
         }
         catch (Exception ex)
         {
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

     private bool RegisterNewUser()
     {
         string username = txtUsername.Text.Trim();
         string password = txtPassword.Text;

         // Generate salt and hash password
         string salt = PasswordHelper.GenerateSalt();
         string passwordHash = PasswordHelper.HashPassword(password, salt);

         using (var connection = Database.Login_Registerdb.GetConnection())
         {
             connection.Open();

             // Check if username already exists
             using (var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", connection))
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

             // Insert new user
             using (var insertCmd = new SQLiteCommand(
                 "INSERT INTO Users (Username, PasswordHash, Salt) VALUES (@username, @passwordHash, @salt)",
                 connection))
             {
                 insertCmd.Parameters.AddWithValue("@username", username);
                 insertCmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                 insertCmd.Parameters.AddWithValue("@salt", salt);

                 int rowsAffected = insertCmd.ExecuteNonQuery();
                 return rowsAffected > 0;
             }
         }
     }

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



