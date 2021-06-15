using BookMarket.Application.Commands.PublisherCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.PublisherCommands
{
    public class EfDeletePublisherCommand : IDeletePublisherCommand
    {
        public readonly BookMarketContext context;

        public EfDeletePublisherCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 2;

        public string Name => "Delete Publisher Command";

        public void Execute(int request)
        {
            var pub = context.Publishers.Find(request);
            if (pub == null) throw new EntityNotFoundException(request, typeof(Publisher));

            pub.IsDeleted = true;
            pub.IsActive = false;
            pub.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
