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
    public class PublishersController : ControllerBase
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public PublishersController(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<PublishersController>
        [HttpGet]
        public IActionResult Get([FromQuery] PublishersSearch dto)
        {
            var publ = context.Publishers.AsQueryable();

            if (dto.Search != null)
            {
                publ = publ.Where(x => x.Name.ToLower().Contains(dto.Search.ToLower()));
            }

            try
            {
                return Ok(mapper.Map<List<PublisherDto>>(publ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<PublishersController>/5
        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id)
        {
            var publ = context.Publishers.Find(id);
            if (publ == null) return NotFound();

            try
            {
                return Ok(mapper.Map<PublisherDto>(publ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<PublishersController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Publisher dto,
            [FromServices] CreatePublisherValidator validator)
        {
            var errors = new List<ClientError>();
            var result = validator.Validate(dto);

            if (!result.IsValid) return result.AsUnprocessabeEntity();

            try
            {
                context.Publishers.Add(dto);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdatePublisherDto dto,
            [FromServices] UpdatePublisherValidator validator)
        {
            var publ = context.Publishers.Find(id);
            if (publ == null) return NotFound();

            var result = validator.Validate(dto);
            if (!result.IsValid) return result.AsUnprocessabeEntity();

            mapper.Map(dto, publ);

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

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publ = context.Publishers.Find(id);
            if (publ == null) return NotFound();

            publ.IsDeleted = true;
            publ.IsActive = false;
            publ.DeletedAt = DateTime.Now;

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
