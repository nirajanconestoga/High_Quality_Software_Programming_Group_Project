using System;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Database.Login_Registerdb.InitializeDatabase();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This should be empty or just open a dropdown
            // Don't set any DialogResult here
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var registerForm = new RegisterForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    // Registration was successful
                    MessageBox.Show("You can now login with your new account");
                    loginToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Update UI for logged in state
                loginToolStripMenuItem.Enabled = false;
                registerToolStripMenuItem.Enabled = false;
                logoutToolStripMenuItem.Enabled = true;

                // After successful login, hide the MainForm and show the DashboardForm
                this.Hide();  // Hide the MainForm

                // Show the DashboardForm to the logged-in user
                DashboardForm dashboard = new DashboardForm();
                dashboard.ShowDialog();  // Show DashboardForm and wait for it to close

                // Optionally, you can show the MainForm again after closing the DashboardForm, or you can exit the application.
                // For now, if you want the MainForm to show again after the dashboard closes, uncomment the next line:
                this.Show();  // Show MainForm again (if required)
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Update UI for logged out state
            loginToolStripMenuItem.Enabled = true;
            registerToolStripMenuItem.Enabled = true;
            logoutToolStripMenuItem.Enabled = false;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Expense Tracker Help\nVersion 1.0", "Help");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Additional help options can be added here
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
