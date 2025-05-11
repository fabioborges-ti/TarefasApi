namespace TarefaApi.Domain.Entities;

public class Tarefa
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool Concluida { get; set; }
}
