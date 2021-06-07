using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.UserQueries
{
    public interface IGetUsersQuery : IQuery<UsersSearch, PagedResponse<UserDto>>
    {
    }
}
