using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace catagorization.Database
{
    // Concept: Static class used for encapsulating DB access logic related to Categories
    public static class Categorizationdb
    {
        // Stores the database path and connection string
        // Concept: File Path Construction + Connection Management
        private static string dbPath = Path.Combine(Application.StartupPath, "Database", "budget.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        // Concept: Initialization Logic + File System Handling + Defensive Programming
        public static void InitializeDatabase()
        {
            try
            {
                // Create Database folder if it doesn't exist
                if (!Directory.Exists(Path.GetDirectoryName(dbPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

                // Create .db file if it doesn't exist
                if (!File.Exists(dbPath))
                    SQLiteConnection.CreateFile(dbPath);

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Concept: SQL Schema Definition
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );";

                    // Concept: SQL Command Execution
                    using (var cmd = new SQLiteCommand(createTableQuery, conn))
                        cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Concept: Error Handling and User Notification
                MessageBox.Show("Categorization DB Init Error: " + ex.Message);
            }
        }

        // Adds a new category to the table
        // Concept: Parameterized Query + SQL INSERT
        public static void AddCategory(string categoryName)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    // INSERT OR IGNORE prevents duplicates (because Name is UNIQUE)
                    string insertQuery = "INSERT OR IGNORE INTO Categories (Name) VALUES (@Name)";
                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", categoryName); // Prevents SQL injection
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Insert Error: " + ex.Message);
            }
        }

        // Updates an existing category's name
        // Concept: SQL UPDATE with parameters
        public static void UpdateCategory(int id, string newName)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                    using (var cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", newName);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Update Error: " + ex.Message);
            }
        }

        // Deletes a category from the database
        // Concept: SQL DELETE
        public static void DeleteCategory(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Categories WHERE Id = @Id";
                    using (var cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Delete Error: " + ex.Message);
            }
        }

        // Fetches all categories and returns them in a DataTable
        // Concept: Data Access Layer + Data Abstraction (returning DataTable)
        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Categories ORDER BY Name ASC";

                    // Concept: DataAdapter for filling DataTable
                    using (var adapter = new SQLiteDataAdapter(selectQuery, conn))
                    {
                        adapter.Fill(dt); // Fills the DataTable with result set
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
