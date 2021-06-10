using AutoMapper;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.UserCommands
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        public readonly BookMarketContext context;
        public readonly UpdateUserValidator validator;
        public readonly IMapper mapper;

        public EfUpdateUserCommand(BookMarketContext context, UpdateUserValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Update User.";

        public void Execute(UpdateUserDto dto)
        {
            var user = context.Users.Find(dto.Id);
            if (user == null) throw new EntityNotFoundException(dto.Id, typeof(User));

            validator.ValidateAndThrow(dto);

            mapper.Map(dto, user);
            context.SaveChanges();
        }
    }
}
