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

namespace BookMarket.Implementation.Commands.UserCommands
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly BookMarketContext context;

        public EfCreateUserCommand(BookMarketContext context)
        {
            this.context = context;
        }

        public int Id => 1;
        public string Name => "Create New User";

        public void Execute(User dto)
        {
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
        }
    }
}
