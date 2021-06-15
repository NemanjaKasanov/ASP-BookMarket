using AutoMapper;
using BookMarket.Application.Commands.CartCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.CartCommands
{
    public class EfUpdateCartCommand : IUpdateCartCommand
    {
        public readonly BookMarketContext context;
        public readonly CreateCartValidator validator;
        public readonly IMapper mapper;

        public EfUpdateCartCommand(BookMarketContext context, CreateCartValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Update Cart Command";

        public void Execute(Cart dto)
        {
            var cart = context.Carts.Find(dto.Id);
            if (cart == null) throw new EntityNotFoundException(dto.Id, typeof(Cart));

            validator.ValidateAndThrow(dto);

            mapper.Map(dto, cart);
            context.SaveChanges();
        }
    }
}
