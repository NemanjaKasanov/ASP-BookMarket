using BookMarket.Application.Commands.PublisherCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.PublisherCommands
{
    public class EfCreatePublisherCommand : ICreatePublisherCommand
    {
        private readonly BookMarketContext context;
        private readonly CreatePublisherValidator validator;

        public EfCreatePublisherCommand(BookMarketContext context, CreatePublisherValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 4;

        public string Name => "Create Publisher Command";

        public void Execute(Publisher dto)
        {
            validator.ValidateAndThrow(dto);
            context.Publishers.Add(dto);
            context.SaveChanges();
        }
    }
}
