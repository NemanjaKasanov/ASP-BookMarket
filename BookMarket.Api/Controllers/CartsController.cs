using AutoMapper;
using BookMarket.Application;
using BookMarket.Application.Commands.CartCommands;
using BookMarket.Application.Queries.CartQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
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
    public class CartsController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public CartsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<CartsController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] CartsSearch dto,
            [FromServices] IGetCartsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<CartsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetCartQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<CartsController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Cart dto,
            [FromServices] ICreateCartCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CartsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody] Cart dto,
            [FromServices] IUpdateCartCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<CartsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteCartCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
