using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskPulse.API.Contracts.Requests;
using TaskPulse.API.Contracts.Responses;
using TaskPulse.Application.Models;
using TaskPulse.Application.Tasks.Commands.CompleteTask;
using TaskPulse.Application.Tasks.Commands.CreateTask;
using TaskPulse.Application.Tasks.Queries.GetTasks;

namespace TaskPulse.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST /api/tasks
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
            [FromForm] CreateTaskRequest request)
        {
            try
            {
                var fileUpload = (FileUpload?)null;

                if (request.File != null)
                    fileUpload = new FileUpload(
                        request.File.FileName,
                        request.File.ContentType,
                        request.File.OpenReadStream()
                    );

                var taskId = await _mediator.Send(
                    new CreateTaskCommand(
                        request.Title,
                        request.SlaHours,
                        fileUpload));

                return CreatedAtAction(
                    nameof(Get),
                    new { id = taskId },
                    null);
            }      
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> Get(
            [FromQuery] bool? completed)
        {
            var tasks = await _mediator.Send(
                new GetTasksQuery(completed));

            var response = tasks.Select(x => new TaskResponse
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt,
                DueAt = x.DueAt,
                IsCompleted = x.IsCompleted,
                IsSlaBreached = x.IsSlaExpired(DateTimeOffset.Now)
            });

            return Ok(response);
        }

        // PUT /api/tasks/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            await _mediator.Send(new CompleteTaskCommand(id));
            return NoContent();
        }        
    }
}
