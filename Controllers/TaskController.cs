using AutoMapper;
using doto.Data;
using doto.Data.Dtos;
using doto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace doto.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private TaskContext _context;
    private IMapper _mapper;

    public TasksController(TaskContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria uma nova tarefa
    /// </summary>
    /// <param name="taskDto">Os dados necessários para a criação de uma tarefa</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] CreateTaskDto taskDto)
    {
        var task = _mapper.Map<TaskModel>(taskDto);
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Show), new { id = task.Id }, task);
    }

    /// <summary>
    /// Obtém todas as tarefas cadastradas
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso e retorne algo</response>
    [HttpGet]
    public IEnumerable<TaskModel> Index(int skip = 0, int take = 10)
    {
        return _context.Tasks.Skip(skip).Take(take);
    }

    /// <summary>
    /// Obtém a tarefa com o id correspondente
    /// </summary>
    /// <param name="id">Um id do tipo UUID</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso e retorne algo</response>
    /// <response code="404">Caso a busca não encontre um registro que corresponda</response>
    [HttpGet("{id}")]
    public IActionResult Show(Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    /// <summary>
    /// Atualiza a tarefa com id correspondente com os dados passados de forma parcial
    /// </summary>
    /// <param name="id">Um id do tipo UUID</param>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso não encontre um registro que corresponda</response>
    /// <response code="400">Caso os dados passados não seguem o padrão especificado</response>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(Guid id, JsonPatchDocument<UpdateTaskDto> patch) 
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        var taskToUpdate = _mapper.Map<UpdateTaskDto>(task);
        patch.ApplyTo(taskToUpdate, ModelState);

        if (!TryValidateModel(taskToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(taskToUpdate, task);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove a tarefa com id correspondente
    /// </summary>
    /// <param name="id">Um id do tipo UUID</param>
    /// <response code="204">Caso a remoção seja feita com sucesso</response>
    /// <response code="404">Caso não encontre um registro que corresponda</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        _context.Remove(task);
        _context.SaveChanges();

        return NoContent();
    }
}
