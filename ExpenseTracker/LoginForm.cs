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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and password are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string selectUser = "SELECT PasswordHash, Salt FROM Users WHERE Username = @username";

                    using (var command = new SQLiteCommand(selectUser, connection))
                    {
                        command.Parameters.AddWithValue("@username", txtUsername.Text);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["PasswordHash"].ToString();
                                string salt = reader["Salt"].ToString();
                                string enteredHash = PasswordHelper.HashPassword(txtPassword.Text, salt);

                                if (storedHash == enteredHash)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
