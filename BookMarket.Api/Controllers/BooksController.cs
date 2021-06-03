using AutoMapper;
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

        public BooksController(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IActionResult Get([FromQuery] BooksSearch dto)
        {
            var books = context.Books.AsQueryable();

            if(dto.Search != null)
            {
                books = books.Where(
                    x => x.Title.ToLower().Contains(dto.Search.ToLower()) ||
                         x.Writer.Name.ToLower().Contains(dto.Search.ToLower()) ||
                         x.Description.ToLower().Contains(dto.Search.ToLower()));
            }

            try
            {
                return Ok(mapper.Map<List<BookDto>>(books));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            var book = context.Books.Find(id);
            if (book == null) return NotFound();

            try
            {
                return Ok(mapper.Map<BookDto>(book));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Book dto,
            [FromServices] CreateBookValidator validator)
        {
            var errors = new List<ClientError>();
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
