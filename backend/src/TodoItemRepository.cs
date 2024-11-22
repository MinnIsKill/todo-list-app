public class TodoItemRepository
{
    private readonly string _connectionString;

    public TodoItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

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

            return await connection.QueryAsync<TodoItem>(query, new { State = state });
        }
    }

    public async Task<TodoItem> GetTodoItemByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<TodoItem>("SELECT * FROM TodoItems WHERE Id = @Id", new { Id = id });
        }
    }

    public async Task AddTodoItemAsync(TodoItem item)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "INSERT INTO TodoItems (Id, Title, Content, State) VALUES (@Id, @Title, @Content, @State)";
            await connection.ExecuteAsync(query, item);
        }
    }

    public async Task UpdateTodoItemAsync(TodoItem item)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "UPDATE TodoItems SET Title = @Title, Content = @Content, State = @State WHERE Id = @Id";
            await connection.ExecuteAsync(query, item);
        }
    }

    public async Task DeleteTodoItemAsync(Guid id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync("DELETE FROM TodoItems WHERE Id = @Id", new { Id = id });
        }
    }
}
