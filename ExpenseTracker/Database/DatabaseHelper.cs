using System.IO;
using System.Windows.Forms;

namespace ExpenseTracker.Database
{
    public static class DatabaseHelper
    {
        // Central location for all DB files: ./Database/
        public static readonly string DatabaseDirectory = Path.Combine(Application.StartupPath, "Database");

        // Make sure the folder exists
        public static void EnsureDatabaseFolderExists()
        {
            if (!Directory.Exists(DatabaseDirectory))
                Directory.CreateDirectory(DatabaseDirectory);
        }

        // Get full path to any .db file
        public static string GetDatabasePath(string fileName)
        {
            EnsureDatabaseFolderExists();
            return Path.Combine(DatabaseDirectory, fileName);
        }

        // Optional: Clear all databases during development
        public static void DeleteAllDatabases()
        {
            if (Directory.Exists(DatabaseDirectory))
            {
                foreach (var file in Directory.GetFiles(DatabaseDirectory, "*.db"))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
