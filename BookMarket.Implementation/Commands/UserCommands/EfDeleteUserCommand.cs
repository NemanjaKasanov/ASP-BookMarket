using AutoMapper;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.UserCommands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteUserCommand(BookMarketContext context, IMapper mapper)
        {
            this.context = context;
        }

        public int Id => 2;

        public string Name => "Delete Group Command";

        public void Execute(int request)
        {
            var user = context.Users.Find(request);
            if (user == null) throw new EntityNotFoundException(request, typeof(User));

            user.IsDeleted = true;
            user.IsActive = false;
            user.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
