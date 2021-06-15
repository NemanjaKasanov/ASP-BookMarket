using AutoMapper;
using BookMarket.Application.Commands.PublisherCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.PublisherCommands
{
    public class EfUpdatePublisherCommand : IUpdatePublisherCommand
    {
        public readonly BookMarketContext context;
        public readonly UpdatePublisherValidator validator;
        public readonly IMapper mapper;

        public EfUpdatePublisherCommand(BookMarketContext context, UpdatePublisherValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Update Publisher Command";

        public void Execute(UpdatePublisherDto dto)
        {
            var pub = context.Publishers.Find(dto.Id);
            if (pub == null) throw new EntityNotFoundException(dto.Id, typeof(Publisher));

            validator.ValidateAndThrow(dto);

            mapper.Map(dto, pub);
            context.SaveChanges();
        }
    }
}
