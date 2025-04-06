using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ExpenseTracker.Database
{
    public class Budgetdb
    {
        // ✅ Use shared helper for consistent DB path
        private static string dbPath = DatabaseHelper.GetDatabasePath("budget.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Settings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Month TEXT NOT NULL,
                            Year TEXT NOT NULL,
                            [Limit] REAL NOT NULL
                        );";

                    var cmd = new SQLiteCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Init Error: " + ex.Message);
            }
        }

        public static bool SaveMonthlyBudget(string month, string year, decimal limit)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM Settings WHERE Month = @Month AND Year = @Year";
                    var delCmd = new SQLiteCommand(deleteQuery, conn);
                    delCmd.Parameters.AddWithValue("@Month", month);
                    delCmd.Parameters.AddWithValue("@Year", year);
                    delCmd.ExecuteNonQuery();

                    string insertQuery = "INSERT INTO Settings (Month, Year, [Limit]) VALUES (@Month, @Year, @Limit)";
                    var cmd = new SQLiteCommand(insertQuery, conn);
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

        public static decimal GetBudgetForMonth(string month, string year)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT [Limit] FROM Settings WHERE Month = @Month AND Year = @Year";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get Budget Error: " + ex.Message);
                return 0;
            }
        }
    }
}
