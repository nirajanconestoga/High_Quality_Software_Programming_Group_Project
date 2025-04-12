using System;
using System.Data;
using System.Data.SQLite;

namespace income_ExpenseTracker.Database
{
    // Static class to handle all database operations related to income
    public static class income_managementdb
    {
        // Get the database file path from a helper (assumed to handle OS paths)
        private static string dbPath = ExpenseTracker.Database.DatabaseHelper.GetDatabasePath("expense_tracker.db");

        // SQLite connection string using the path
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        /// <summary>
        /// Creates and returns a new SQLite connection object
        /// </summary>
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        /// <summary>
        /// Creates the Income table if it doesn't exist
        /// Called during app startup to ensure schema is ready
        /// </summary>
        public static void InitializeDatabase()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Income (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, -- Unique ID for each income entry
                        Source TEXT NOT NULL,                 -- Income source name (e.g., Salary)
                        Amount REAL NOT NULL,                -- Amount of income
                        Date TEXT NOT NULL                   -- Date of income in string format
                    );";

                // Execute SQL command to create table
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Inserts a new income record into the database
        /// </summary>
        /// <param name="source">Income source name</param>
        /// <param name="amount">Amount of income</param>
        /// <param name="date">Date the income was received</param>
        public static void AddIncome(string source, double amount, DateTime date)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Income (Source, Amount, Date) VALUES (@source, @amount, @date)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    // Use parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@source", source);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd")); // ISO format
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves the full list of income entries as a DataTable
        /// Used for binding to the DataGridView
        /// </summary>
        public static DataTable GetIncomeList()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Income";
                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt); // Fill DataTable with query results
                    return dt;
                }
            }
        }

        /// <summary>
        /// Updates an existing income entry in the database
        /// </summary>
        /// <param name="id">The ID of the income record to update</param>
        /// <param name="source">New source value</param>
        /// <param name="amount">New amount value</param>
        /// <param name="date">New date value</param>
        public static void UpdateIncome(int id, string source, double amount, DateTime date)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE Income SET Source = @source, Amount = @amount, Date = @date WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@source", source);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery(); // Execute update
                }
            }
        }

        /// <summary>
        /// Deletes an income record based on its ID
        /// </summary>
        /// <param name="id">The ID of the income to delete</param>
        public static void DeleteIncome(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Income WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery(); // Execute deletion
                }
            }
        }

        /// <summary>
        /// Calculates the total income for a given month and year
        /// Useful for summary dashboards and reports
        /// </summary>
        /// <param name="month">Month in MM format</param>
        /// <param name="year">Year in YYYY format</param>
        /// <returns>Total income as decimal</returns>
        public static decimal GetTotalIncomeForMonth(string month, string year)
        {
            decimal total = 0;

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT SUM(Amount) 
                    FROM Income 
                    WHERE strftime('%m', Date) = @month AND strftime('%Y', Date) = @year";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    // Pad month with 0 if necessary (e.g., "4" => "04")
                    cmd.Parameters.AddWithValue("@month", month.PadLeft(2, '0'));
                    cmd.Parameters.AddWithValue("@year", year);

                    // Use ExecuteScalar for single value result
                    var result = cmd.ExecuteScalar();
                    total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            return total;
        }
    }
}
