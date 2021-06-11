using BookMarket.Application.Commands.WriterCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.WriterCommands
{
    public class EfDeleteWriterCommand : IDeleteWriterCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteWriterCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete Writer Command";

        public void Execute(int request)
        {
            var writer = context.Writers.Find(request);
            if (writer == null) throw new EntityNotFoundException(request, typeof(Writer));

            writer.IsDeleted = true;
            writer.IsActive = false;
            writer.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
