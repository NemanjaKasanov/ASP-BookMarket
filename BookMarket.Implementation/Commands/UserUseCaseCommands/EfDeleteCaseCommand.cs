using BookMarket.Application.Commands.UserUseCaseCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.UserUseCaseCommands
{
    public class EfDeleteCaseCommand : IDeleteCaseCommand
    {

        public readonly BookMarketContext context;

        public EfDeleteCaseCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete UserUseCase Command";

        public void Execute(int request)
        {
            var useCase = context.UserUseCase.Find(request);
            if (useCase == null) throw new EntityNotFoundException(request, typeof(UserUseCase));
            context.UserUseCase.Remove(useCase);
            context.SaveChanges();
        }
    }
}
