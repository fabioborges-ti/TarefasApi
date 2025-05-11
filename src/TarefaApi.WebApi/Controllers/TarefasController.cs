using Microsoft.AspNetCore.Mvc;
using TarefaApi.Application.Services;
using TarefaApi.Domain.Entities;

namespace TarefaApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly TarefaService _service;

    public TarefasController(TarefaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.ObterTodasAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var tarefa = await _service.ObterPorIdAsync(id);
        return tarefa == null ? NotFound() : Ok(tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Tarefa tarefa)
    {
        await _service.AdicionarAsync(tarefa);
        return CreatedAtAction(nameof(Get), new { id = tarefa.Id }, tarefa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, Tarefa tarefa)
    {
        if (id != tarefa.Id) return BadRequest();
        await _service.AtualizarAsync(tarefa);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.RemoverAsync(id);
        return NoContent();
    }
}