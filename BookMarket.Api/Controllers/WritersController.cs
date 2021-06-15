using AutoMapper;
using BookMarket.Application;
using BookMarket.Application.Commands.GenreCommands;
using BookMarket.Application.Commands.WriterCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Queries.GenreQueries;
using BookMarket.Application.Queries.WriterQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Extensions;
using BookMarket.Implementation.Validators;
using Microsoft.AspNetCore.Authorization;
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
    public class WritersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public WritersController(IMapper mapper, IApplicationActor actor, UseCaseExecutor executor)
        {
            this.mapper = mapper;
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<WritersController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] WritersSearch dto,
            [FromServices] IGetWritersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<WritersController>/5
        [HttpGet("{id}", Name = "GetWriter")]
        public IActionResult Get(int id, [FromServices] IGetWriterQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<WritersController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(
            [FromBody] Writer dto,
            [FromServices] ICreateWriterCommand command)
        {
            Writer writer = mapper.Map<Writer>(dto);
            executor.ExecuteCommand(command, writer);
            return StatusCode(StatusCodes.Status201Created);

        }

        // PUT api/<WritersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateWriterDto dto,
            [FromServices] IUpdateWriterCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<WritersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteWriterCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
