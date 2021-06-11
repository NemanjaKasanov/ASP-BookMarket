using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.WriterQueries
{
    public interface IGetWriterQuery : IQuery<int, WriterDto>
    {
    }
}
