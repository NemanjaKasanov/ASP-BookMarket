using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.UserQueries
{
    public interface IGetUserQuery : IQuery<int, UserDto>
    {
    }
}
