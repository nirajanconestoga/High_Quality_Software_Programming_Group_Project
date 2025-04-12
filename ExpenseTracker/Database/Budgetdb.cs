using System;
using System.Data.SQLite;
using System.Windows.Forms;
using ExpenseTracker.Database;

namespace budget.Database
{
    public class Budgetdb
    {
        // Use shared helper for consistent DB path
        // Gets a consistent file path for storing the SQLite database using a helper class.
        private static string dbPath = DatabaseHelper.GetDatabasePath("budget.db");

        // SQLite connection string pointing to the database file
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        // Initializes the database by creating the Settings table if it doesn't exist
        public static void InitializeDatabase()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open(); // Opens a connection to the SQLite DB

                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Settings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Month TEXT NOT NULL,
                            Year TEXT NOT NULL,
                            [Limit] REAL NOT NULL
                        );";

                    // Create table command execution
                    var cmd = new SQLiteCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery(); // Executes the table creation query
                }
            }
            catch (Exception ex)
            {
                // Show message box if database initialization fails
                MessageBox.Show("DB Init Error: " + ex.Message);
            }
        }

        // Saves a monthly budget by deleting existing entry for the same month/year and inserting new one
        public static bool SaveMonthlyBudget(string month, string year, decimal limit)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Delete any existing budget record for the same month and year (to avoid duplicates)
                    string deleteQuery = "DELETE FROM Settings WHERE Month = @Month AND Year = @Year";
                    var delCmd = new SQLiteCommand(deleteQuery, conn);
                    delCmd.Parameters.AddWithValue("@Month", month);
                    delCmd.Parameters.AddWithValue("@Year", year);
                    delCmd.ExecuteNonQuery();

                    // Insert the new budget limit
                    string insertQuery = "INSERT INTO Settings (Month, Year, [Limit]) VALUES (@Month, @Year, @Limit)";
                    var cmd = new SQLiteCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Limit", limit);
                    cmd.ExecuteNonQuery();

                    return true; // Return true if everything went fine
                }
            }
            catch (Exception ex)
            {
                // Show message box if saving fails
                MessageBox.Show("Save Budget Error: " + ex.Message);
                return false;
            }
        }

        // Retrieves the budget limit for a specific month and year
        public static decimal GetBudgetForMonth(string month, string year)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch the budget limit
                    string query = "SELECT [Limit] FROM Settings WHERE Month = @Month AND Year = @Year";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        var result = cmd.ExecuteScalar(); // ExecuteScalar returns a single value
                        return result != null ? Convert.ToDecimal(result) : 0; // Return 0 if no result found
                    }
                }
            }
            catch (Exception ex)
            {
                // Show message box if retrieval fails
                MessageBox.Show("Get Budget Error: " + ex.Message);
                return 0;
            }
        }
    }
}
