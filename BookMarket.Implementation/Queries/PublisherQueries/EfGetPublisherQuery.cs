using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.PublisherQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Queries.PublisherCommands
{
    public class EfGetPublisherQuery : IGetPublisherQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetPublisherQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Publisher";

        public PublisherDto Execute(int search)
        {
            var pub = context.Publishers.Find(search);
            if (pub == null) throw new EntityNotFoundException(search, typeof(Publisher));
            return mapper.Map<PublisherDto>(pub);
        }
    }
}
