using BookMarket.Application.Commands.OrderCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Commands.OrderCommands
{
    public class EfDeleteOrderCommand : IDeleteOrderCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteOrderCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete Order Command";

        public void Execute(int request)
        {
            var order = context.Books.Find(request);
            if (order == null) throw new EntityNotFoundException(request, typeof(Order));

            order.IsDeleted = true;
            order.IsActive = false;
            order.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
