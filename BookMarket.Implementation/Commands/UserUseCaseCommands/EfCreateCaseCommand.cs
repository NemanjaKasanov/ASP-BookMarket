using BookMarket.Application.Commands.UserUseCaseCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.UserUseCaseCommands
{
    public class EfCreateCaseCommand : ICreateCaseCommand
    {
        private readonly BookMarketContext context;

        public EfCreateCaseCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Create UserUseCase Command";

        public void Execute(UserUseCase dto)
        {
            context.UserUseCase.Add(dto);
            context.SaveChanges();
        }
    }
}
