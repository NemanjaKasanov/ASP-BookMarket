using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.PublisherQueries
{
    public interface IGetPublishersQuery : IQuery<PublishersSearch, PagedResponse<PublisherDto>>
    {
    }
}
