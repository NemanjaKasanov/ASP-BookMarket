using BookMarket.Application.DataTransfer;
using BookMarket.Application.Queries.UserUseCasesQueries;
using BookMarket.Application.Searches;
using BookMarket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Queries.UserUseCaseQueries
{
    public class EfGetCasesQuery : IGetCasesQuery
    {
        private readonly BookMarketContext context;

        public EfGetCasesQuery(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 2;

        public string Name => "Get UserUseCases Query";

        public PagedResponse<UserUseCaseDto> Execute(CasesSearch dto)
        {
            var books = context.UserUseCase.AsQueryable();

            if (dto.UserId.HasValue)
            {
                books = books.Where(x => x.UserId == dto.UserId);
            }

            if (dto.UseCaseId.HasValue)
            {
                books = books.Where(x => x.UseCaseId == dto.UseCaseId);
            }

            var skipCount = dto.PerPage * (dto.Page - 1);
            var response = new PagedResponse<UserUseCaseDto>
            {
                CurrentPage = dto.Page,
                ItemsPerPage = dto.PerPage,
                TotalCount = books.Count(),
                Items = books.Skip(skipCount).Take(dto.PerPage).Select(x => new UserUseCaseDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UseCaseId = x.UseCaseId,
                    User = new UserDto
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Username = x.User.Username,
                        Email = x.User.Email,
                        Address = x.User.Address,
                        Phone = x.User.Phone
                    }
                }).ToList()
            };
            return response;
        }
    }
}
