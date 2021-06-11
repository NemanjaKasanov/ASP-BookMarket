using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.BookQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.BookQueries
{
    public class EfGetBooksQuery : IGetBooksQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetBooksQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Books Query";

        public PagedResponse<BookDto> Execute(BooksSearch dto)
        {
            var books = context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Search) || !string.IsNullOrWhiteSpace(dto.Search))
                books = books.Where(x => x.Title.ToLower().Contains(dto.Search.ToLower()) ||
                                         x.Writer.Name.ToLower().Contains(dto.Search.ToLower()) ||
                                         x.Description.ToLower().Contains(dto.Search.ToLower()));

            if (dto.PublisherId.HasValue)
            {
                books = books.Where(x => x.PublisherId == dto.PublisherId);
            }

            if (dto.PriceMin.HasValue)
            {
                books = books.Where(x => x.Price >= dto.PriceMin);
            }

            if (dto.PriceMax.HasValue)
            {
                books = books.Where(x => x.Price <= dto.PriceMax);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);
            var response = new PagedResponse<BookDto>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = books.Count(),
                Items = books.Skip(skipCount).Take(dto.PerPage).Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Writer = new WriterDto
                    {
                        Id = x.Writer.Id,
                        Name = x.Writer.Name
                    },
                    Year = x.Year,
                    Price = x.Price,
                    Pages = x.Pages,
                    Publisher = new PublisherDto
                    {
                        Id = x.Publisher.Id,
                        Name = x.Publisher.Name
                    },
                    Genres = x.BookGenres.Select(y => new GenreDto 
                    { 
                        Id = y.GenreId,
                        Name = y.Genre.Name
                    })
                }).ToList()
            };
            return response;
        }
    }
}
