using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.GenreQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
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
            var genre = context.Genres.Find(search);
            if (genre == null) throw new EntityNotFoundException(search, typeof(Genre));
            return mapper.Map<GenreDto>(genre);
        }
    }
}
