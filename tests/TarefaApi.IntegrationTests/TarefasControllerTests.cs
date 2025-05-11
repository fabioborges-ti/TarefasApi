using System.Net;
using System.Net.Http.Json;
using TarefaApi.Domain.Entities;

public class TarefasControllerTests : IClassFixture<PostgreSqlContainerFixture>
{
    private readonly HttpClient _client;

    public TarefasControllerTests(PostgreSqlContainerFixture fixture)
    {
        var factory = new CustomWebApplicationFactory(fixture.Container.GetConnectionString());

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

    [Fact]
    public async Task Get_NonExistent_Tarefa_Should_Return_NotFound()
    {
        var nonExistentId = Guid.NewGuid(); // ID que não existe no banco

        var response = await _client.GetAsync($"/api/tarefas/{nonExistentId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
