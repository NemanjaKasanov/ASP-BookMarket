using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.PublisherQueries
{
    public interface IGetPublisherQuery : IQuery<int, PublisherDto>
    {
    }
}
