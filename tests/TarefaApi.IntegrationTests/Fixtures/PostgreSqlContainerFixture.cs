using Testcontainers.PostgreSql;

public class PostgreSqlContainerFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; }

    public async Task InitializeAsync()
    {
        Container = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("admin")
            .WithPassword("aline123")
            .Build();

        await Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }
}
