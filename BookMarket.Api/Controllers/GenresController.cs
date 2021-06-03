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
    public class GenresController : ControllerBase
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public GenresController(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public IActionResult Get([FromQuery] GenresSearch dto)
        {
            var genres = context.Genres.AsQueryable();

            if (dto.Search != null)
            {
                genres = genres.Where(x => x.Name.ToLower().Contains(dto.Search.ToLower()));
            }

            try
            {
                return Ok(mapper.Map<List<GenreDto>>(genres));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}", Name = "GetGenre")]
        public IActionResult Get(int id)
        {
            var genre = context.Genres.Find(id);
            if (genre == null) return NotFound();

            try
            {
                return Ok(mapper.Map<GenreDto>(genre));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<GenresController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] Genre dto,
            [FromServices] CreateGenreValidator validator)
        {
            var errors = new List<ClientError>();
            var result = validator.Validate(dto);

            if (!result.IsValid) return result.AsUnprocessabeEntity();

            try
            {
                context.Genres.Add(dto);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateGenreDto dto,
            [FromServices] UpdateGenreValidator validator)
        {
            var genre = context.Genres.Find(id);
            if (genre == null) return NotFound();

            var result = validator.Validate(dto);
            if (!result.IsValid) return result.AsUnprocessabeEntity();

            mapper.Map(dto, genre);

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

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genre = context.Genres.Find(id);
            if (genre == null) return NotFound();

            genre.IsDeleted = true;
            genre.IsActive = false;
            genre.DeletedAt = DateTime.Now;

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
