using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ExpenseTracker
{
   

    public static class DatabaseHelper
    {
        private static string connectionString = "Data Source=ExpenseTracker.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists("ExpenseTracker.db"))
            {
                SQLiteConnection.CreateFile("ExpenseTracker.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create Users table
                    string createUsersTable = @"
                CREATE TABLE Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT UNIQUE NOT NULL,
                    PasswordHash TEXT NOT NULL,
                    Salt TEXT NOT NULL,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                )";

                    new SQLiteCommand(createUsersTable, connection).ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
