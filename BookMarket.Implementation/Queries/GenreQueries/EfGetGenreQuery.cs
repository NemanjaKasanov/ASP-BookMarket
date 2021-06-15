using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.GenreQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.GenreQueries
{
    public class EfGetGenreQuery : IGetGenreQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetGenreQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Genre";

        public GenreDto Execute(int search)
        {
            var genre = context.Genres.Include(x => x.BookGenres).ThenInclude(x => x.Book).FirstOrDefault(x => x.Id == search);
            if (genre == null) throw new EntityNotFoundException(search, typeof(Genre));

            var ret = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Books = genre.BookGenres.Select(x => new BookDto
                {
                    Id = x.Book.Id,
                    Title = x.Book.Title,
                    Description = x.Book.Description,
                    Year = x.Book.Year,
                    Price = x.Book.Price,
                    Pages = x.Book.Pages,
                    Writer = mapper.Map<WriterDto>(context.Writers.Find(x.Book.WriterId)),
                    Publisher = mapper.Map<PublisherDto>(context.Publishers.Find(x.Book.PublisherId))
                })
            };
            return ret;
        }
    }
}
