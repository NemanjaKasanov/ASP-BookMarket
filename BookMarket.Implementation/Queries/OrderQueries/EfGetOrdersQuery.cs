using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.OrdersQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.OrderQueries
{
    public class EfGetOrdersQuery : IGetOrdersQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetOrdersQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Orders Query";

        public PagedResponse<OrderDto> Execute(OrdersSearch dto)
        {
            var orders = context.Orders.AsQueryable();

            if (dto.OrderId.HasValue)
            {
                orders = orders.Where(x => x.Id >= dto.OrderId);
            }

            if (dto.UserId.HasValue)
            {
                orders = orders.Where(x => x.UserId >= dto.UserId);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);
            var response = new PagedResponse<OrderDto>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = orders.Count(),
                Items = orders.Skip(skipCount).Take(dto.PerPage).Select(x => new OrderDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = new UserDto
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Username = x.User.Username,
                        Email = x.User.Email,
                        Address = x.User.Address,
                        Phone = x.User.Phone
                    },
                    OrderBooks = x.OrderBooks.Select(book => new OrderBook
                    {
                        OrderId = book.OrderId,
                        BookId = book.BookId,
                        Quantity = book.Quantity
                    })
                }).ToList()
            };
            return response;
        }
    }
}
