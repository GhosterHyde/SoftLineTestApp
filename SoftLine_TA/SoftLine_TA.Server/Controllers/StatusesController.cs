using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftLine_TA.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftLine_TA.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly ILogger<StatusesController> _logger;
        private readonly TaskContext taskContext;

        public StatusesController(ILogger<StatusesController> logger, TaskContext taskContext)
        {
            _logger = logger;
            this.taskContext = taskContext;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            var statuses = taskContext.Statuses.ToList();
            var innerStatuses = statuses.Select(x => new
            {
                statusId = x.Status_ID,
                statusName = x.Status_name,
            }).ToArray();
            return innerStatuses;
        }
    }
}
