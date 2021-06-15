using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Queries.UserUseCasesQueries
{
    public interface IGetCasesQuery : IQuery<CasesSearch, PagedResponse<UserUseCaseDto>>
    {
    }
}
