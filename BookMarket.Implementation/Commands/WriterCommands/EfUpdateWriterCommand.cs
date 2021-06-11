using AutoMapper;
using BookMarket.Application.Commands.WriterCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.WriterCommands
{
    public class EfUpdateWriterCommand : IUpdateWriterCommand
    {
        public readonly BookMarketContext context;
        public readonly UpdateWriterValidator validator;
        public readonly IMapper mapper;

        public EfUpdateWriterCommand(BookMarketContext context, UpdateWriterValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Update Writer";

        public void Execute(UpdateWriterDto dto)
        {
            var writer = context.Writers.Find(dto.Id);
            if (writer == null) throw new EntityNotFoundException(dto.Id, typeof(Writer));

            validator.ValidateAndThrow(dto);

            mapper.Map(dto, writer);
            context.SaveChanges();
        }
    }
}
