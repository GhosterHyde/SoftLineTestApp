using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoftLine_TA.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftLine_TA.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly TaskContext taskContext;

        public TaskController(ILogger<TaskController> logger, TaskContext taskContext)
        {
            _logger = logger;
            this.taskContext = taskContext;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            var tasks = taskContext.Tasks.Include(x => x.Status).ToArray();
            var innerTasks = tasks.Select(x => new
            {
                id = x.ID,
                name = x.Name,
                description = x.Description,
                statusName = x.Status?.Status_name
            }).ToArray();
            return innerTasks;
        }

        [HttpGet("/task/{id:int}")]
        public object GetTask(int id)
        {
            var task = taskContext.Tasks.Include(x => x.Status).FirstOrDefault(x => x.ID == id);
            var innerTasks = new
            {
                id = task.ID,
                name = task.Name,
                description = task.Description,
                statusId = task.Status?.Status_ID
            };
            return innerTasks;
        }

        [HttpPost("/task/create")]
        public IActionResult Post([FromBody] Task innerTask)
        {
            var status = taskContext.Statuses.Find(innerTask.Status_ID);
            Task task = new Task()
            {
                Name = innerTask.Name,
                Description = innerTask.Description,
                Status = status,
                Status_ID = innerTask.Status_ID
            };
            taskContext.Tasks.Add(task);

            taskContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, task);
        }

        [HttpPut("/task/update")]
        public IActionResult Put([FromBody] Task innerTask)
        {
            Task task = taskContext.Tasks.FirstOrDefault(x => x.ID == innerTask.ID);

            task.Name = innerTask.Name;
            task.Description = innerTask.Description;

            var status = taskContext.Statuses.Find(innerTask.Status_ID);
            task.Status = status;
            task.Status_ID = innerTask.Status_ID;

            taskContext.SaveChanges();
            return Ok(task);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var task = taskContext.Tasks.Find(id);
            if (task != null)
            {
                taskContext.Tasks.Remove(task);
                taskContext.SaveChanges();
            }
        }
    }
}
