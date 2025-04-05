using System;
using System.Windows.Forms;
using ExpenseTracker.Database;

namespace ExpenseTracker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Enable visual styles and other application settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the database (if needed)
            DatabaseHelper.InitializeDatabase();

            // Show the MainForm (Login/Register form) when the app starts
            Application.Run(new MainForm());
        }
    }
}
