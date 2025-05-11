using Microsoft.EntityFrameworkCore;
using TarefaApi.Domain.Entities;
using TarefaApi.Domain.Interfaces;
using TarefaApi.Infrastructure.Context;

namespace TarefaApi.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefaDbContext _context;

    public TarefaRepository(TarefaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tarefa>> ObterTodasAsync() => await _context.Tarefas.ToListAsync();

    public async Task<Tarefa?> ObterPorIdAsync(Guid id) => await _context.Tarefas.FindAsync(id);

    public async Task AdicionarAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        var tarefa = await ObterPorIdAsync(id);
        if (tarefa != null)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
