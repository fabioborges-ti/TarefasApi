using Microsoft.EntityFrameworkCore;
using TarefaApi.Domain.Entities;

namespace TarefaApi.Infrastructure.Context;

public class TarefaDbContext : DbContext
{
    public TarefaDbContext(DbContextOptions<TarefaDbContext> options) : base(options) { }
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
}