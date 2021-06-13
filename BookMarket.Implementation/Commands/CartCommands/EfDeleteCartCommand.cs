using BookMarket.Application.Commands.CartCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.CartCommands
{
    public class EfDeleteCartCommand : IDeleteCartCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteCartCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete Cart Command";

        public void Execute(int request)
        {
            var cart = context.Carts.Find(request);
            if (cart == null) throw new EntityNotFoundException(request, typeof(Cart));

            cart.IsDeleted = true;
            cart.IsActive = false;
            cart.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
