using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.GenreQueries
{
    public interface IGetGenreQuery : IQuery<int, GenreDto>
    {
    }
}
