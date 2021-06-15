using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.LogQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.LogQueries
{
    public class EfGetLogsQuery : IGetLogsQuery
    {
        private readonly BookMarketContext context;

        public EfGetLogsQuery(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Get Logs Query";

        public PagedResponse<UseCaseLogDto> Execute(LogsSearch dto)
        {
            var logs = context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(dto.Search) || !string.IsNullOrWhiteSpace(dto.Search))
                logs = logs.Where(x => x.UseCaseName.ToLower().Contains(dto.Search.ToLower()) ||
                                       x.Actor.ToLower().Contains(dto.Search.ToLower()));

            var skipCount = dto.PerPage * (dto.Page - 1);
            var response = new PagedResponse<UseCaseLogDto>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = logs.Count(),
                Items = logs.Skip(skipCount).Take(dto.PerPage).Select(x => new UseCaseLogDto
                {
                    Id = x.Id,
                    UseCaseName = x.UseCaseName,
                    Data = x.Data,
                    Actor = x.Actor,
                    Date = x.Date
                }).ToList()
            };
            return response;
        }
    }
}
