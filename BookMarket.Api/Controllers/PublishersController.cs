using AutoMapper;
using BookMarket.Application;
using BookMarket.Application.Commands.PublisherCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Queries.PublisherQueries;
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
    public class PublishersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public PublishersController(IMapper mapper, UseCaseExecutor executor, IApplicationActor actor)
        {
            this.mapper = mapper;
            this.executor = executor;
            this.actor = actor;
        }

        // GET: api/<PublishersController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] PublishersSearch dto,
            [FromServices] IGetPublishersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<PublishersController>/5
        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id, [FromServices] IGetPublisherQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<PublishersController>
        [Authorize]
        [HttpPost]
        public IActionResult Post(
            [FromBody] Publisher dto,
            [FromServices] ICreatePublisherCommand command)
        {
            Publisher pub = mapper.Map<Publisher>(dto);
            executor.ExecuteCommand(command, pub);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PublishersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdatePublisherDto dto,
            [FromServices] IUpdatePublisherCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<PublishersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeletePublisherCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
