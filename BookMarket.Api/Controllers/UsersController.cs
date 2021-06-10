using AutoMapper;
using Bogus;
using BookMarket.Application;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Email;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Queries.UserQueries;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public UsersController(IMapper mapper, IApplicationActor actor, UseCaseExecutor executor)
        {
            this.mapper = mapper;
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] UsersSearch dto,
            [FromServices] IGetUsersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id, [FromServices] IGetUserQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] User dto,
            [FromServices] ICreateUserCommand command)
        {
            User user = mapper.Map<User>(dto);
            executor.ExecuteCommand(command, user);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<UsersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody] UpdateUserDto dto,
            [FromServices] IUpdateUserCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteUserCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}


/*var usersFaker = new Faker<User>();
usersFaker.RuleFor(x => x.FirstName, name => name.Name.FirstName());
usersFaker.RuleFor(x => x.LastName, lname => lname.Name.LastName());
usersFaker.RuleFor(x => x.Username, username => username.Internet.UserName());
usersFaker.RuleFor(x => x.Email, email => email.Internet.Email());
usersFaker.RuleFor(x => x.Password, pass => pass.Internet.Password());
usersFaker.RuleFor(x => x.Phone, phone => phone.Phone.PhoneNumber());
usersFaker.RuleFor(x => x.Address, address => address.Address.FullAddress());
usersFaker.RuleFor(x => x.Role, 0);
var users = usersFaker.Generate(100);
context.Users.AddRange(users);
context.SaveChanges();*/
