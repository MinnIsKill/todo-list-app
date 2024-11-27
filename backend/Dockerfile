# Use .NET SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY backend.csproj .
RUN dotnet restore

# Copy the rest of backend files
COPY . .

# Copy the database explicitly
COPY database/todoapp.db /app/database/

# Build the project
RUN dotnet publish -c Release -o /out

# Use the ASP.NET Core runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Expose port 5029
EXPOSE 5029

# Copy the built project from the build image
COPY --from=build /out .

# Set the entry point to the application
ENTRYPOINT ["dotnet", "backend.dll"]