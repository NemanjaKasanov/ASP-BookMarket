using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Searches;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.CartQueries
{
    public interface IGetCartsQuery : IQuery<CartsSearch, PagedResponse<CartDto>>
    {
    }
}
