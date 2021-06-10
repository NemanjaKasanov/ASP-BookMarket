using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.UserQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.UserQueries
{
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly BookMarketContext context;
        private readonly IMapper mapper;

        public EfGetUsersQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Users Search.";

        public PagedResponse<UserDto> Execute(UsersSearch search)
        {
            var users = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search.Search) || !string.IsNullOrWhiteSpace(search.Search)) 
                users = users.Where(x => x.FirstName.ToLower().Contains(search.Search.ToLower()) ||
                                         x.LastName.ToLower().Contains(search.Search.ToLower()) ||
                                         x.Username.ToLower().Contains(search.Search.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);
            var response = new PagedResponse<UserDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = users.Count(),
                Items = users.Skip(skipCount).Take(search.PerPage).Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Username = x.Username,
                    Address = x.Address,
                    Phone = x.Phone
                }).ToList()
            };
            return response;
        }
    }
}
