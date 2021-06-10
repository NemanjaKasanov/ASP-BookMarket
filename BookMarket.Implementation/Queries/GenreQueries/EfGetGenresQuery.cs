using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.GenreQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.GenreQueries
{
    public class EfGetGenresQuery : IGetGenresQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetGenresQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Genres";

        public PagedResponse<GenreDto> Execute(GenresSearch search)
        {
            var genres = context.Genres.AsQueryable();
            if(!string.IsNullOrEmpty(search.Search) || !string.IsNullOrWhiteSpace(search.Search))
                genres = genres.Where(x => x.Name.ToLower().Contains(search.Search.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);
            var response = new PagedResponse<GenreDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = genres.Count(),
                Items = genres.Skip(skipCount).Take(search.PerPage).Select(x => new GenreDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            return response;
        }
    }
}
