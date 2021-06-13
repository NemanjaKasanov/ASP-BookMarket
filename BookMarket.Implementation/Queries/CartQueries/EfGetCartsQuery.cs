using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.CartQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.CartQueries
{
    public class EfGetCartsQuery : IGetCartsQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetCartsQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Get Carts Query";

        public PagedResponse<CartDto> Execute(CartsSearch dto)
        {
            var carts = context.Carts.AsQueryable();

            if (dto.BookId.HasValue)
            {
                carts = carts.Where(x => x.BookId == dto.BookId);
            }

            if (dto.UserId.HasValue)
            {
                carts = carts.Where(x => x.UserId >= dto.UserId);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);
            var response = new PagedResponse<CartDto>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = carts.Count(),
                Items = carts.Skip(skipCount).Take(dto.PerPage).Select(x => new CartDto
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    UserId = x.UserId,
                    BookId = x.BookId,
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
                    Book = new BookDto
                    {
                        Id = x.Book.Id,
                        Title = x.Book.Title,
                        Description = x.Book.Description,
                        Year = x.Book.Year,
                        Pages = x.Book.Pages,
                        Price = x.Book.Price
                    }
                }).ToList()
            };
            return response;
        }
    }
}
