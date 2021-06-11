using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.PublisherQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.PublisherCommands
{
    public class EfGetPublishersQuery : IGetPublishersQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetPublishersQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Publishers";

        public PagedResponse<PublisherDto> Execute(PublishersSearch search)
        {
            var pub = context.Publishers.AsQueryable();
            if (!string.IsNullOrEmpty(search.Search) || !string.IsNullOrWhiteSpace(search.Search))
                pub = pub.Where(x => x.Name.ToLower().Contains(search.Search.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);
            var response = new PagedResponse<PublisherDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = pub.Count(),
                Items = pub.Skip(skipCount).Take(search.PerPage).Select(x => new PublisherDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            return response;
        }
    }
}
