using BookMarket.Application.Commands.CartCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.CartCommands
{
    public class EfCreateCartCommand : ICreateCartCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateCartValidator validator;

        public EfCreateCartCommand(BookMarketContext context, CreateCartValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 1;

        public string Name => "Create Cart Command";

        public void Execute(Cart dto)
        {
            validator.ValidateAndThrow(dto);
            context.Carts.Add(dto);
            context.SaveChanges();
        }
    }
}
