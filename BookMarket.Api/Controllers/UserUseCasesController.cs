using BookMarket.Application;
using BookMarket.Application.Commands.UserUseCaseCommands;
using BookMarket.Application.Queries.UserUseCasesQueries;
using BookMarket.Application.Searches;
using BookMarket.Domain;
using Microsoft.AspNetCore.Http;
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
    public class UserUseCasesController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public UserUseCasesController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<UserUseCasesController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] CasesSearch dto,
            [FromServices] IGetCasesQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // POST api/<UserUseCasesController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] UserUseCase dto,
            [FromServices] ICreateCaseCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE api/<UserUseCasesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteCaseCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
