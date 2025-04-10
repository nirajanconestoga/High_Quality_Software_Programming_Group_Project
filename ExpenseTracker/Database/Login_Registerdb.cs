using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ExpenseTracker.Database
{


    // This is a static class that handles database operations for login and registration functionality
    // Static class concept: Cannot be instantiated and contains only static members
    public static class Login_Registerdb
    {
        // Connection string for SQLite database
        // Connection string concept: Contains information needed to establish a connection to the database
        // SQLite stores data in a single file (ExpenseTracker.db in this case)
        private static string connectionString = "Data Source=ExpenseTracker.db;Version=3;";

        // Method to initialize the database and create tables if they don't exist
        // Database initialization concept: Ensures the database structure is ready before operations
        public static void InitializeDatabase()
        {
            // Check if the database file doesn't exist
            // File existence check concept: Prevents overwriting existing database
            if (!File.Exists("ExpenseTracker.db"))
            {
                // Create a new SQLite database file
                // Database file creation concept: SQLite creates a physical file for the database
                SQLiteConnection.CreateFile("ExpenseTracker.db");

                // Using statement ensures proper disposal of resources
                // Resource management concept: Ensures connection is properly closed
                using (var connection = new SQLiteConnection(connectionString))
                {
                    // Open the database connection
                    // Connection lifecycle concept: Must be opened before executing commands
                    connection.Open();

                    // SQL command to create Users table
                    // Table creation concept: Defines the structure for storing user data
                    string createUsersTable = @"
                CREATE TABLE Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,  // Auto-incrementing primary key
                    Username TEXT UNIQUE NOT NULL,      // Unique username constraint
                    PasswordHash TEXT NOT NULL,          // Stores hashed password
                    Salt TEXT NOT NULL,                  // Salt for password hashing
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP  // Automatic timestamp
                )";

                    // Execute the table creation command
                    // SQL command execution concept: Sending DDL (Data Definition Language) to database
                    new SQLiteCommand(createUsersTable, connection).ExecuteNonQuery();
                }
            }
        }

        // Method to get a new database connection
        // Connection factory concept: Centralizes connection creation
        public static SQLiteConnection GetConnection()
        {
            // Returns a new connection object
            // Object creation concept: Each call creates a new connection instance
            return new SQLiteConnection(connectionString);
        }
    }
}
