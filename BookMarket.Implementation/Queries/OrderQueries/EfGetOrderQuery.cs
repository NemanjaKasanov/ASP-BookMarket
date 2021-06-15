using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.OrdersQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.OrderQueries
{
    public class EfGetOrderQuery : IGetOrderQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetOrderQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Get Order Query";

        public OrderDto Execute(int request)
        {
            var order = context.Orders.Find(request);
            if (order == null) throw new EntityNotFoundException(request, typeof(Order));

            var user = context.Users.Find(order.UserId);
            var books = context.OrderBooks.Where(x => x.OrderId == request);

            var ret = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone
                },
                OrderBooks = books.Select(x => new OrderBook
                {
                    OrderId = x.OrderId,
                    BookId = x.BookId,
                    Quantity = x.Quantity
                })
            };
            return ret;
        }
    }
}
