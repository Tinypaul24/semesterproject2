using System;
using System.IO;
using System.Data.SQLite;
using System.Threading.Tasks;


namespace EntityInserter
{
    public static class DataManager
    {
        public static string DatabasePath { get; set; }
        public static async Task LoadFiles()
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;  // Go to the project root
                string databasePath = Path.Combine(projectDirectory, "Models", "Data", "SDM.sqlite");

                Console.WriteLine($"Database Path: {databasePath}");
                DatabasePath = databasePath;
                // Create the directory if it doesn't exist
                string directoryPath = Path.GetDirectoryName(databasePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory created at: {directoryPath}");
                }

                string connectionString = $"Data Source={databasePath};";

                // Ensure the database file exists
                if (!File.Exists(databasePath))
                {
                    Console.WriteLine("Database file not found. Creating a new one...");

                    await Task.Run(() =>
                    {
                        SQLiteConnection.CreateFile(databasePath);
                        Console.WriteLine("Database file creation task completed.");
                    });

                    if (File.Exists(databasePath))
                    {
                        Console.WriteLine("Database file created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the database file.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static string GetDatabasePath()
        {
            return DatabasePath;
        }
        public static void EnsureTablesExist(SQLiteConnection connection)
        {
            string createWinterTableQuery = @"
                CREATE TABLE IF NOT EXISTS WinterTimeData (
                    DataFromTime TEXT NOT NULL,
                    DataToTime TEXT NOT NULL,
                    HeatDemand REAL NOT NULL,
                    EletricityPrice REAL NOT NULL
                );";

            string createSummerTableQuery = @"
                CREATE TABLE IF NOT EXISTS SummerTimeData (
                    DataFromTime TEXT,
                    DataToTime TEXT,
                    HeatDemand REAL NOT NULL,
                    EletricityPrice REAL NOT NULL
                );";

            using (var command = new SQLiteCommand(createWinterTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createSummerTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}