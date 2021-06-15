using BookMarket.Application.Commands.WriterCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.WriterCommands
{
    public class EfCreateWriterCommand : ICreateWriterCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateWriterValidator validator;

        public EfCreateWriterCommand(BookMarketContext context, CreateWriterValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Writer";

        public void Execute(Writer dto)
        {
            validator.ValidateAndThrow(dto);
            context.Writers.Add(dto);
            context.SaveChanges();
        }
    }
}
