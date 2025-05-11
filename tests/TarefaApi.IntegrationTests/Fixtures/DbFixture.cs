using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TarefaApi.IntegrationTests.Fixtures;

public class DbFixture : IDisposable
{
    public string DatabaseName { get; }
    public string ConnectionString => $"{_baseConnectionString};Database={DatabaseName}";
    private readonly string _adminConnectionString;
    private readonly string _baseConnectionString;

    public DbFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _adminConnectionString = configuration.GetConnectionString("DefaultConnection")!;
        _baseConnectionString = _adminConnectionString[.._adminConnectionString.LastIndexOf("Database=", StringComparison.OrdinalIgnoreCase)];

        DatabaseName = $"test_db_{Guid.NewGuid():N}";

        using var conn = new NpgsqlConnection(_adminConnectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = $"CREATE DATABASE \"{DatabaseName}\"";
        cmd.ExecuteNonQuery();
    }

    public void Dispose()
    {
        using var conn = new NpgsqlConnection(_adminConnectionString);
        conn.Open();

        using var terminateCmd = conn.CreateCommand();
        terminateCmd.CommandText = $@"
            SELECT pg_terminate_backend(pid)
            FROM pg_stat_activity
            WHERE datname = '{DatabaseName}' AND pid <> pg_backend_pid();";
        terminateCmd.ExecuteNonQuery();

        using var dropCmd = conn.CreateCommand();
        dropCmd.CommandText = $"DROP DATABASE IF EXISTS \"{DatabaseName}\"";
        dropCmd.ExecuteNonQuery();
    }
}
