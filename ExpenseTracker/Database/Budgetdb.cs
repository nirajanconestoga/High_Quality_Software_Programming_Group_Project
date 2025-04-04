using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace ExpenseTracker.Database
{
    public class Budgetdb
    {
        // Runtime database directory inside bin/Debug/netX.X-windows/Database/
        private static string dbDirectory = Path.Combine(Application.StartupPath, "Database");
        private static string dbPath = Path.Combine(dbDirectory, "budget.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        /// <summary>
        /// Initializes the database: creates folder, file, and Settings table
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                // Create folder if it doesn't exist
                if (!Directory.Exists(dbDirectory))
                    Directory.CreateDirectory(dbDirectory);

                // Create database file if it doesn't exist
                if (!File.Exists(dbPath))
                    SQLiteConnection.CreateFile(dbPath);

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Create table: Settings (with reserved word [Limit] handled)
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Settings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Month TEXT NOT NULL,
                            Year TEXT NOT NULL,
                            [Limit] REAL NOT NULL
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

        /// <summary>
        /// Saves or updates the monthly budget for a given month and year
        /// </summary>
        public static bool SaveMonthlyBudget(string month, string year, decimal limit)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Remove existing entry for the same month & year (if any)
                    string deleteQuery = "DELETE FROM Settings WHERE Month = @Month AND Year = @Year";
                    SQLiteCommand delCmd = new SQLiteCommand(deleteQuery, conn);
                    delCmd.Parameters.AddWithValue("@Month", month);
                    delCmd.Parameters.AddWithValue("@Year", year);
                    delCmd.ExecuteNonQuery();

                    // Insert new budget record
                    string insertQuery = "INSERT INTO Settings (Month, Year, [Limit]) VALUES (@Month, @Year, @Limit)";
                    SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Limit", limit);
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Budget Error: " + ex.Message);
                return false;
            }
        }
    }
}
