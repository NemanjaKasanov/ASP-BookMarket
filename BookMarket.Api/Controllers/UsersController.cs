using AutoMapper;
using Bogus;
using BookMarket.Application;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Extensions;
using BookMarket.Implementation.Validators;
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
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public UsersController(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UsersSearch dto)
        {
            var users = context.Users.AsQueryable();

            if (dto.Search != null) users = users.Where(
                x => x.FirstName.ToLower().Contains(dto.Search.ToLower()) ||
                     x.LastName.ToLower().Contains(dto.Search.ToLower()) ||
                     x.Username.ToLower().Contains(dto.Search.ToLower()));

            try
            {
                return Ok(mapper.Map<List<UserDto>>(users));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) return NotFound();

            try
            {
                return Ok(mapper.Map<UserDto>(user));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] User dto,
            [FromServices] CreateUserValidator validator)
        {
            var errors = new List<ClientError>();
            var result = validator.Validate(dto);

            if (!result.IsValid) return result.AsUnprocessabeEntity();

            MD5 hash = MD5.Create();
            byte[] pass = hash.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            StringBuilder strBuilder = new StringBuilder();
            for(int i = 0; i < pass.Length; i++)
            {
                strBuilder.Append(pass[i].ToString("x2"));
            }
            dto.Password = strBuilder.ToString();

            try
            {
                context.Users.Add(dto);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody] UpdateUserDto dto,
            [FromServices] UpdateUserValidator validator)
        {
            var user = context.Users.Find(id);
            if (user == null) return NotFound();

            var result = validator.Validate(dto);
            if (!result.IsValid) return result.AsUnprocessabeEntity();

            mapper.Map(dto, user);

            try
            {
                context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) return NotFound();

            user.IsDeleted = true;
            user.IsActive = false;
            user.DeletedAt = DateTime.Now;

            try
            {
                context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
