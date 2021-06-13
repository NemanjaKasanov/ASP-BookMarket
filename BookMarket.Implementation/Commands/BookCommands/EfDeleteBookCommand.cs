using BookMarket.Application.Commands.BookCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.BookCommands
{
    public class EfDeleteBookCommand : IDeleteBookCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteBookCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete Book Command";

        public void Execute(int request)
        {
            var book = context.Books.Find(request);
            if (book == null) throw new EntityNotFoundException(request, typeof(Book));

            book.IsDeleted = true;
            book.IsActive = false;
            book.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
