using System;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Data.SQLite;
using EntityInserter;

namespace DataBaseContext
{
    public class CsvDataLoader
    {
        public async static void LoadCsvFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine("Invalid file path.");
                    return;
                }

                // Ensure the database and table are set up
                await Task.Run(() => DataManager.LoadFiles());

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // The CSV has a header row
                    Delimiter = ",", // Adjust delimiter if necessary
                    MissingFieldFound = null,
                    BadDataFound = context =>
                    {
                        Console.WriteLine($"Bad data found: {context.RawRecord}");
                    },
                    TrimOptions = TrimOptions.Trim, // Trim spaces from fields
                };

                using (var file_reader = new StreamReader(filePath))
                {
                    var file_csv = new CsvReader(file_reader, config);

                    // Read the header row
                    file_csv.Read();
                    file_csv.ReadHeader();

                    string databasePath = DataManager.GetDatabasePath();
                    string connectionString = $"Data Source={databasePath};";

                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Database connection opened successfully.");
                        
                        // Ensure tables exist
                        DataManager.EnsureTablesExist(connection);

                        // Clear tables
                        ClearTables(connection);
                        Console.WriteLine("Database tables cleared successfully.");
                        
                        while (file_csv.Read())
                        {
                            // Skip rows with empty or irrelevant data
                            if (string.IsNullOrWhiteSpace(file_csv.GetField(0)) || file_csv.GetField(0).Contains("Winter period"))
                                continue;

                            // Process Winter Period Data
                            if (!string.IsNullOrWhiteSpace(file_csv.GetField(0)))
                            {
                                try
                                {
                                    var winterRecord = new WinterData
                                    {
                                        DataFromTime = file_csv.GetField(0).Trim(),
                                        DataToTime = file_csv.GetField(1).Trim(),
                                        HeatDemand = double.TryParse(file_csv.GetField(2).Trim(), out var heatDemand) ? heatDemand : 0.0,
                                        EletricityPrice = double.TryParse(file_csv.GetField(3).Trim(), out var electricityPrice) ? electricityPrice : 0.0
                                    };

                                    string insertQuery = @"
                                        INSERT INTO WinterTimeData (DataFromTime, DataToTime, HeatDemand, EletricityPrice)
                                        VALUES (@DataFromTime, @DataToTime, @HeatDemand, @EletricityPrice);";

                                    using (var command = new SQLiteCommand(insertQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@DataFromTime", winterRecord.DataFromTime);
                                        command.Parameters.AddWithValue("@DataToTime", winterRecord.DataToTime);
                                        command.Parameters.AddWithValue("@HeatDemand", winterRecord.HeatDemand);
                                        command.Parameters.AddWithValue("@EletricityPrice", winterRecord.EletricityPrice);

                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error processing winter row: {file_csv.Context.Parser.RawRecord}. Error: {ex.Message}");
                                }
                            }

                            // Process Summer Period Data
                            if (!string.IsNullOrWhiteSpace(file_csv.GetField(5)))
                            {
                                try
                                {
                                    var summerRecord = new SummerData
                                    {
                                        DataFromTime = file_csv.GetField(5).Trim(),
                                        DataToTime = file_csv.GetField(6).Trim(),
                                        HeatDemand = double.TryParse(file_csv.GetField(7).Trim(), out var heatDemand) ? heatDemand : 0.0,
                                        EletricityPrice = double.TryParse(file_csv.GetField(8).Trim(), out var electricityPrice) ? electricityPrice : 0.0
                                    };

                                    string insertQuery = @"
                                        INSERT INTO SummerTimeData (DataFromTime, DataToTime, HeatDemand, EletricityPrice)
                                        VALUES (@DataFromTime, @DataToTime, @HeatDemand, @EletricityPrice);";

                                    using (var command = new SQLiteCommand(insertQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@DataFromTime", summerRecord.DataFromTime);
                                        command.Parameters.AddWithValue("@DataToTime", summerRecord.DataToTime);
                                        command.Parameters.AddWithValue("@HeatDemand", summerRecord.HeatDemand);
                                        command.Parameters.AddWithValue("@EletricityPrice", summerRecord.EletricityPrice);

                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error processing summer row: {file_csv.Context.Parser.RawRecord}. Error: {ex.Message}");
                                }
                            }
                        }

                        Console.WriteLine("CSV data inserted into the database successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading CSV file: {ex.Message}");
            }
        }
        private static void ClearTables(SQLiteConnection connection)
        {
            string clearWinterTableQuery = "DELETE FROM WinterTimeData;";
            string clearSummerTableQuery = "DELETE FROM SummerTimeData;";

            using (var command = new SQLiteCommand(clearWinterTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(clearSummerTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Database tables cleared successfully.");
        }
    }
}