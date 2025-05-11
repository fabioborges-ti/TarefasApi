using TarefaApi.Domain.Entities;

namespace TarefaApi.Domain.Interfaces;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> ObterTodasAsync();
    Task<Tarefa?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(Tarefa tarefa);
    Task AtualizarAsync(Tarefa tarefa);
    Task RemoverAsync(Guid id);
}
