using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.BookQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.BookQueries
{
    public class EfGetBookQuery : IGetBookQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetBookQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Book Query";

        public BookDto Execute(int request)
        {
            //var book = context.Books.Find(request);
            var book = context.Books.Include(x => x.BookGenres).ThenInclude(x => x.Genre).FirstOrDefault(x => x.Id == request); ;
            if (book == null) throw new EntityNotFoundException(request, typeof(Book));

            WriterDto writer = mapper.Map<WriterDto>(context.Writers.Find(book.WriterId));
            PublisherDto publisher = mapper.Map<PublisherDto>(context.Publishers.Find(book.PublisherId));

            var ret = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Year = book.Year,
                Price = book.Price,
                Pages = book.Pages,
                Writer = writer,
                Publisher = publisher,
                Genres = book.BookGenres.Select(x => new GenreDto 
                { 
                    Id = x.Genre.Id,
                    Name = x.Genre.Name
                })
            };
            return ret;
        }
    }
}
