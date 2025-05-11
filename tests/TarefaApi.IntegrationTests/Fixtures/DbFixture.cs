using Npgsql;

namespace TarefaApi.IntegrationTests.Fixtures;

public class DbFixture : IDisposable
{
    public string DatabaseName { get; }
    public string ConnectionString => $"Host=localhost;Username=admin;Password=aline123;Database={DatabaseName}";

    public DbFixture()
    {
        DatabaseName = $"test_db_{Guid.NewGuid():N}";

        using var conn = new NpgsqlConnection("Host=localhost;Username=admin;Password=aline123;Database=postgres");
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = $"CREATE DATABASE \"{DatabaseName}\"";
        cmd.ExecuteNonQuery();
    }

    public void Dispose()
    {
        using var conn = new NpgsqlConnection("Host=localhost;Username=admin;Password=aline123;Database=postgres");
        conn.Open();

        // Termina conexões pendentes e remove o banco
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
