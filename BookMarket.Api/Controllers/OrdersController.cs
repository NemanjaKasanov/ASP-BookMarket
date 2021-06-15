using BookMarket.Application;
using BookMarket.Application.Commands.OrderCommands;
using BookMarket.Application.Queries.OrdersQueries;
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
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public OrdersController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] OrdersSearch dto,
            [FromServices] IGetOrdersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetOrderQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Order dto,
            [FromServices] ICreateOrderCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody] Order dto,
            [FromServices] IUpdateOrderCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteOrderCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
