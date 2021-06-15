using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.CartQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Queries.CartQueries
{
    public class EfGetCartQuery : IGetCartQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetCartQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get Cart Query";

        public CartDto Execute(int search)
        {
            var cart = context.Carts.Find(search);
            if (cart == null) throw new EntityNotFoundException(search, typeof(Cart));

            var user = context.Users.Find(cart.UserId);
            var book = context.Books.Find(cart.BookId);

            var ret = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                BookId = cart.BookId,
                Quantity = cart.Quantity,
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
                Book = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Year = book.Year,
                    Pages = book.Pages,
                    Price = book.Price
                }
            };
            return ret;
        }
    }
}
