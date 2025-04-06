using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using ExpenseTracker.Database;

namespace Expense_Tracker.Database
{
    public class Categorizationdb
    {
        private static string dbPath = DatabaseHelper.GetDatabasePath("budget.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );";
                    SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Init Error: " + ex.Message);
            }
        }

        public static void AddCategory(string categoryName)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Ensure table exists before insert (failsafe)
                    string ensureTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );";
                    new SQLiteCommand(ensureTableQuery, conn).ExecuteNonQuery();

                    string insertQuery = "INSERT OR IGNORE INTO Categories (Name) VALUES (@Name)";
                    SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Name", categoryName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Insert Error: " + ex.Message);
            }
        }

        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Categories ORDER BY Name ASC";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Read Error: " + ex.Message);
            }

            return dt;
        }
    }
}
