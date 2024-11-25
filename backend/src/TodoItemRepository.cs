using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using TodoApi.Models; //for TodoItem
using Microsoft.Extensions.Logging;

namespace TodoApi.Repositories
{
    public class TodoItemRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TodoItemRepository> _logger;

        public TodoItemRepository(string connectionString, ILogger<TodoItemRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        // Method to initialize the database (create the table if it doesn't exist)
        public async Task InitializeDatabaseAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Print out the actual connection string (path)
                Console.WriteLine($"Using SQLite database at: {_connectionString}");

                var createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS TodoItems (
                        Id TEXT PRIMARY KEY,
                        Title TEXT NOT NULL,
                        Content TEXT,
                        State INTEGER NOT NULL
                    );";
                await connection.ExecuteAsync(createTableQuery);

                // Check if the table was created or exists
                var checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='TodoItems';";
                var tableExists = await connection.QueryFirstOrDefaultAsync<string>(checkTableQuery);

                if (tableExists != null)
                {
                    Console.WriteLine("TodoItems table exists.");
                }
                else
                {
                    Console.WriteLine("TodoItems table was not created.");
                }
            }
        }

        // Method to retrieve all Todo items
        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync(int? state = null)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM TodoItems";
                if (state.HasValue)
                {
                    query += " WHERE State = @State";
                }

                var todoItems = await connection.QueryAsync<TodoItem>(query, new { State = state });

                // Log the fetched data
                foreach (var item in todoItems)
                {
                    Console.WriteLine($"Fetched TodoItem: {item.Title}, State: {item.State}");
                }

                return todoItems;
            }
        }

        // Method to retrieve a single Todo item by its Id
        public async Task<TodoItem> GetTodoItemByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Explicitly convert `id` to string if needed
                return await connection.QueryFirstOrDefaultAsync<TodoItem>(
                    "SELECT * FROM TodoItems WHERE Id = @Id",
                    new { Id = id.ToString() }
                );
            }
        }

        // Method to add a Todo item
        public async Task AddTodoItemAsync(TodoItem item)
        {
            _logger.LogInformation("Attempting to add a new TodoItem with ID: {Id}", item.Id);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO TodoItems (Id, Title, Content, State) VALUES (@Id, @Title, @Content, @State)";
                await connection.ExecuteAsync(query, item);

                _logger.LogInformation("TodoItem with ID: {Id} added successfully.", item.Id);
            }
        }

        // Method to update a Todo item
        public async Task UpdateTodoItemAsync(TodoItem item)
        {
            _logger.LogInformation("Attempting to update a TodoItem with ID: {id}", item.Id);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE TodoItems SET Title = @Title, Content = @Content, State = @State WHERE Id = @Id";
                await connection.ExecuteAsync(query, item);

                _logger.LogInformation("TodoItem with ID: {id} deleted successfully.", item.Id);
            }
        }

        // Method to delete a Todo item
        public async Task DeleteTodoItemAsync(Guid id)
        {
            _logger.LogInformation("Attempting to delete a TodoItem with ID: {id}", id);

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM TodoItems WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id.ToString() });

                _logger.LogInformation("TodoItem with ID: {id} deleted successfully.", id);
            }
        }
    }
}