using System;
using System.Data.SQLite;
using System.IO;

public class Database
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SQLiteConnection GetConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    // Method to initialize the database
    public void InitializeDatabase()
    {
        // Ensure the database file exists
        if (!File.Exists("database/todoapp.db"))
        {
            Console.WriteLine("Database not found, creating a new one.");
            SQLiteConnection.CreateFile("database/todoapp.db");

            // Run the schema script
            using (var connection = GetConnection())
            using (var cmd = new SQLiteCommand(File.ReadAllText("database/schema.sql"), connection))
            {
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Database initialized successfully.");
        }
    }
}