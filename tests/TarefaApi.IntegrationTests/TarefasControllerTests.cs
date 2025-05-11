using System.Net.Http.Json;
using TarefaApi.Domain.Entities;
using TarefaApi.IntegrationTests.Factories;
using TarefaApi.IntegrationTests.Fixtures;

namespace TarefaApi.IntegrationTests;

public class TarefasControllerTests : IClassFixture<DbFixture>
{
    private readonly HttpClient _client;

    public TarefasControllerTests(DbFixture dbFixture)
    {
        var factory = new CustomWebApplicationFactory(dbFixture.ConnectionString);
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_And_Get_Tarefa_Works()
    {
        var tarefa = new Tarefa { Id = Guid.NewGuid(), Titulo = "Teste", Concluida = false };

        var post = await _client.PostAsJsonAsync("/api/tarefas", tarefa);
        post.EnsureSuccessStatusCode();

        var get = await _client.GetAsync($"/api/tarefas/{tarefa.Id}");
        var result = await get.Content.ReadFromJsonAsync<Tarefa>();

        Assert.NotNull(result);
        Assert.Equal("Teste", result!.Titulo);
    }
}
