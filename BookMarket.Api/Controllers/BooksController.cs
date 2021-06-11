using AutoMapper;
using BookMarket.Application;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.BookQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Extensions;
using BookMarket.Implementation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;
        private readonly UseCaseExecutor executor;

        public BooksController(BookMarketContext context, IMapper mapper, UseCaseExecutor executor)
        {
            this.context = context;
            this.mapper = mapper;
            this.executor = executor;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] BooksSearch dto,
            [FromServices] IGetBooksQuery query)
        {
            return Ok(executor.ExecuteQuery(query, dto));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(
            int id,
            [FromServices] IGetBookQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Book dto,
            [FromServices] CreateBookValidator validator)
        {
            var result = validator.Validate(dto);

            if (!result.IsValid) return result.AsUnprocessabeEntity();

            try
            {
                context.Books.Add(dto);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
