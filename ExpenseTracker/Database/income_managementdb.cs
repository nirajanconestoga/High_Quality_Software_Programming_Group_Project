using System;
using System.Data;
using System.Data.SQLite;

namespace ExpenseTracker.Database
{
    public static class income_managementdb
    {
        private static string dbPath = DatabaseHelper.GetDatabasePath("expense_tracker.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Income (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Source TEXT NOT NULL,
                        Amount REAL NOT NULL,
                        Date TEXT NOT NULL
                    );";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddIncome(string source, double amount, DateTime date)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Income (Source, Amount, Date) VALUES (@source, @amount, @date)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@source", source);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetIncomeList()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Income";
                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

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
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteIncome(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Income WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static decimal GetTotalIncomeForMonth(string month, string year)
        {
            decimal total = 0;

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT SUM(Amount) FROM Income WHERE strftime('%m', Date) = @month AND strftime('%Y', Date) = @year";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month.PadLeft(2, '0'));
                    cmd.Parameters.AddWithValue("@year", year);
                    var result = cmd.ExecuteScalar();
                    total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            return total;
        }
    }
}
