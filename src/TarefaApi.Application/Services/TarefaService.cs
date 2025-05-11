using TarefaApi.Domain.Entities;
using TarefaApi.Domain.Interfaces;

namespace TarefaApi.Application.Services;

public class TarefaService
{
    private readonly ITarefaRepository _repositorio;

    public TarefaService(ITarefaRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<IEnumerable<Tarefa>> ObterTodasAsync() => _repositorio.ObterTodasAsync();
    public Task<Tarefa?> ObterPorIdAsync(Guid id) => _repositorio.ObterPorIdAsync(id);
    public Task AdicionarAsync(Tarefa tarefa) => _repositorio.AdicionarAsync(tarefa);
    public Task AtualizarAsync(Tarefa tarefa) => _repositorio.AtualizarAsync(tarefa);
    public Task RemoverAsync(Guid id) => _repositorio.RemoverAsync(id);
}
