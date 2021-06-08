using BookMarket.Application;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Extensions;
using BookMarket.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using BookMarket.Application.Email;

namespace BookMarket.Implementation.Commands.UserCommands
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateUserValidator validator;
        private readonly IEmailSender sender;

        public EfCreateUserCommand(BookMarketContext context, CreateUserValidator validator, IEmailSender sender)
        {
            this.context = context;
            this.validator = validator;
            this.sender = sender;
        }

        public int Id => 4;
        public string Name => "Create New User";

        public void Execute(User dto)
        {
            validator.ValidateAndThrow(dto);

            MD5 hash = MD5.Create();
            byte[] pass = hash.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < pass.Length; i++)
            {
                strBuilder.Append(pass[i].ToString("x2"));
            }
            dto.Password = strBuilder.ToString();

            context.Users.Add(dto);
            context.SaveChanges();

            sender.Send(new SendEmailDto
            {
                Content = "<h1>BookMarket: Thank You for Joining Us!</h1><p>You have successfully created a new BookMarket account.</p>",
                SendTo = dto.Email,
                Subject = "BookMarket Registration."
            });
        }
    }
}
