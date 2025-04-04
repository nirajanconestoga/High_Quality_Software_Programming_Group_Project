
﻿using System;
using System.Windows.Forms;
using ExpenseTracker.Database;

namespace ExpenseTracker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            income_managementdb.InitializeDatabase(); // << Call this
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

