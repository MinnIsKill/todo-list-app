using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Get API base URL from configuration or fallback to localhost
var apiBaseUrl = builder.Configuration.GetValue<string>("API_BASE_URL", "https://localhost:5029");

// Add CORS policy to allow frontend to communicate with backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5173")  // Frontend's URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add controllers and Swagger services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database connection and TodoItemRepository
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
}

builder.Services.AddScoped<TodoItemRepository>((serviceProvider) =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<TodoItemRepository>>();
    return new TodoItemRepository(connectionString, logger);
});

var app = builder.Build();

// Ensure the database and tables are initialized on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var repository = services.GetRequiredService<TodoItemRepository>();
    await repository.InitializeDatabaseAsync();  // Ensure database is initialized
}

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");  // Apply CORS policy here

app.MapControllers();

app.Run();