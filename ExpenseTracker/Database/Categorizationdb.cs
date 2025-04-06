using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Expense_Tracker.Database
{
    public static class Categorizationdb
    {
        private static string dbPath = Path.Combine(Application.StartupPath, "Database", "budget.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(dbPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

                if (!File.Exists(dbPath))
                    SQLiteConnection.CreateFile(dbPath);

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );";

                    using (var cmd = new SQLiteCommand(createTableQuery, conn))
                        cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Categorization DB Init Error: " + ex.Message);
            }
        }

        public static void AddCategory(string categoryName)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = "INSERT OR IGNORE INTO Categories (Name) VALUES (@Name)";
                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", categoryName);
                        cmd.ExecuteNonQuery();
                    }
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
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Categories ORDER BY Name ASC";
                    using (var adapter = new SQLiteDataAdapter(selectQuery, conn))
                    {
                        adapter.Fill(dt);
                    }
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
