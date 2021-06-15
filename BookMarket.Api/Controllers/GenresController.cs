using AutoMapper;
using BookMarket.Application;
using BookMarket.Application.Commands.GenreCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Queries.GenreQueries;
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
    public class GenresController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly IMapper mapper;
        private readonly UseCaseExecutor executor;

        public GenresController(IMapper mapper, IApplicationActor actor, UseCaseExecutor executor)
        {
            this.mapper = mapper;
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] GenresSearch dto,
            [FromServices] IGetGenresQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}", Name = "GetGenre")]
        public IActionResult Get(int id, [FromServices] IGetGenreQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<GenresController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(
            [FromBody] Genre dto,
            [FromServices] ICreateGenreCommand command)
        {
            Genre genre = mapper.Map<Genre>(dto);
            executor.ExecuteCommand(command, genre);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<GenresController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateGenreDto dto,
            [FromServices] IUpdateGenreCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<GenresController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteGenreCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
