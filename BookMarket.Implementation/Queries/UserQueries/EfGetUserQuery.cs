using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.Application.Queries.UserQueries;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Queries.UserQueries
{
    public class EfGetUserQuery : IGetUserQuery
    {
        private readonly BookMarketContext context;
        public readonly IMapper mapper;

        public EfGetUserQuery(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get User.";

        public UserDto Execute(int request)
        {
            var user = context.Users.Find(request);
            if (user == null) throw new EntityNotFoundException(request, typeof(User));
            return mapper.Map<UserDto>(user);
        }
    }
}
