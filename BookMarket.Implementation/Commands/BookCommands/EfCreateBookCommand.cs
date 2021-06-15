using BookMarket.Application.Commands.BookCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.BookCommands
{
    public class EfCreateBookCommand : ICreateBookCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateBookValidator validator;

        public EfCreateBookCommand(BookMarketContext context, CreateBookValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Book Command";

        public void Execute(Book dto)
        {
            validator.ValidateAndThrow(dto);
            context.Books.Add(dto);
            context.SaveChanges();
        }
    }
}
