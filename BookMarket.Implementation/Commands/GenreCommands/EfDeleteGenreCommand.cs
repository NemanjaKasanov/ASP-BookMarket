using BookMarket.Application.Commands.GenreCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.GenreCommands
{
    public class EfDeleteGenreCommand : IDeleteGenreCommand
    {
        public readonly BookMarketContext context;

        public EfDeleteGenreCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 4;

        public string Name => "Delete Genre Command";

        public void Execute(int request)
        {
            var genre = context.Genres.Find(request);
            if (genre == null) throw new EntityNotFoundException(request, typeof(Genre));

            genre.IsDeleted = true;
            genre.IsActive = false;
            genre.DeletedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
