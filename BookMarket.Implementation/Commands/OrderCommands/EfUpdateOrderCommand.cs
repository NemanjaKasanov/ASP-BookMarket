using AutoMapper;
using BookMarket.Application.Commands.OrderCommands;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Commands.OrderCommands
{
    public class EfUpdateOrderCommand : IUpdateOrderCommand
    {
        public readonly BookMarketContext context;
        public readonly CreateOrderValidator validator;
        public readonly IMapper mapper;

        public EfUpdateOrderCommand(BookMarketContext context, CreateOrderValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Update Order Command";

        public void Execute(Order dto)
        {
            if (context.Orders.Find(dto.Id) == null) throw new EntityNotFoundException(dto.Id, typeof(Order));
            validator.ValidateAndThrow(dto);
            if (context.Carts.Where(x => x.UserId == dto.UserId).Count() <= 0) throw new EmptyCartException();

            var to_delete = context.OrderBooks.Where(x => x.OrderId == dto.Id);
            foreach(var el in to_delete)
            {
                context.OrderBooks.Remove(el);
            }

            var books = context.Carts.Where(x => x.UserId == dto.UserId);
            foreach (var el in books)
            {
                context.OrderBooks.Add(new OrderBook
                {
                    OrderId = dto.Id,
                    BookId = el.BookId,
                    Quantity = el.Quantity
                });

                el.IsDeleted = true;
                el.IsActive = false;
                el.DeletedAt = DateTime.Now;
            }
            context.SaveChanges();
        }
    }
}
