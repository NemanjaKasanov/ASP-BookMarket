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
    public class WritersController : ControllerBase
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public WritersController(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<WritersController>
        [HttpGet]
        public IActionResult Get([FromQuery] WritersSearch dto)
        {
            var writers = context.Writers.AsQueryable();

            if (dto.Search != null)
            {
                writers = writers.Where(x => x.Name.ToLower().Contains(dto.Search.ToLower()));
            }

            try
            {
                return Ok(mapper.Map<List<WriterDto>>(writers));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<WritersController>/5
        [HttpGet("{id}", Name = "GetWriter")]
        public IActionResult Get(int id)
        {
            var writer = context.Writers.Find(id);
            if (writer == null) return NotFound();

            try
            {
                return Ok(mapper.Map<WriterDto>(writer));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<WritersController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Writer dto,
            [FromServices] CreateWriterValidator validator)
        {
            var errors = new List<ClientError>();
            var result = validator.Validate(dto);

            if (!result.IsValid) return result.AsUnprocessabeEntity();

            try
            {
                context.Writers.Add(dto);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<WritersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateWriterDto dto,
            [FromServices] UpdateWriterValidator validator)
        {
            var writer = context.Writers.Find(id);
            if (writer == null) return NotFound();

            var result = validator.Validate(dto);
            if (!result.IsValid) return result.AsUnprocessabeEntity();

            mapper.Map(dto, writer);

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

        // DELETE api/<WritersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var writer = context.Writers.Find(id);
            if (writer == null) return NotFound();

            writer.IsDeleted = true;
            writer.IsActive = false;
            writer.DeletedAt = DateTime.Now;

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
