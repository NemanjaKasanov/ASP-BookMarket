using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.OrdersQueries
{
    public interface IGetOrderQuery : IQuery<int, OrderDto>
    {
    }
}
