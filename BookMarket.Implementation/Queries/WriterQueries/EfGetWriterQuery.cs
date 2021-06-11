using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.WriterQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Queries.WriterQueries
{
    public class EfGetWriterQuery : IGetWriterQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetWriterQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Writer";

        public WriterDto Execute(int search)
        {
            var writer = context.Writers.Find(search);
            if (writer == null) throw new EntityNotFoundException(search, typeof(Writer));
            return mapper.Map<WriterDto>(writer);
        }
    }
}
