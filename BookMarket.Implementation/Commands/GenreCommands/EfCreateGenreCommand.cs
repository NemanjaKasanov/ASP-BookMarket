using BookMarket.Application.Commands.GenreCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.GenreCommands
{
    public class EfCreateGenreCommand : ICreateGenreCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateGenreValidator validator;

        public EfCreateGenreCommand(BookMarketContext context, CreateGenreValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 4;

        public string Name => "Create Genre";

        public void Execute(Genre dto)
        {
            validator.ValidateAndThrow(dto);
            context.Genres.Add(dto);
            context.SaveChanges();
        }
    }
}
