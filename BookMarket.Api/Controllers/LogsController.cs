using BookMarket.Application;
using BookMarket.Application.Queries.LogQueries;
using BookMarket.Application.Searches;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public LogsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<LogsController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] LogsSearch dto,
            [FromServices] IGetLogsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }
    }
}
