namespace ExpenseTracker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize database before showing main form
            DatabaseHelper.InitializeDatabase();

            Application.Run(new MainForm());
        }
    }
}