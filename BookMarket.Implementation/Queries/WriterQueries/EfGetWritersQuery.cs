using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.WriterQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.WriterQueries
{
    public class EfGetWritersQuery : IGetWritersQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetWritersQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Writers";

        public PagedResponse<WriterDto> Execute(WritersSearch search)
        {
            var writers = context.Writers.AsQueryable();
            if (!string.IsNullOrEmpty(search.Search) || !string.IsNullOrWhiteSpace(search.Search))
                writers = writers.Where(x => x.Name.ToLower().Contains(search.Search.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);
            var response = new PagedResponse<WriterDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = writers.Count(),
                Items = writers.Skip(skipCount).Take(search.PerPage).Select(x => new WriterDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            return response;
        }
    }
}
