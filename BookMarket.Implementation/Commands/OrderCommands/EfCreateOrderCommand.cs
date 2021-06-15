using BookMarket.Application.Commands.OrderCommands;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookMarket.Application.Exceptions;

namespace BookMarket.Implementation.Commands.OrderCommands
{
    public class EfCreateOrderCommand : ICreateOrderCommand
    {
        private readonly BookMarketContext context;
        private readonly CreateOrderValidator validator;

        public EfCreateOrderCommand(BookMarketContext context, CreateOrderValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Order Command";

        public void Execute(Order dto)
        {
            Order order = new Order
            {
                UserId = dto.UserId
            };

            validator.ValidateAndThrow(order);
            if (context.Carts.Where(x => x.UserId == order.UserId).Count() <= 0) throw new EmptyCartException();
            context.Orders.Add(order);
            context.SaveChanges();

            int currentOrderId = order.Id;
            var books = context.Carts.Where(x => x.UserId == order.UserId);

            foreach(var el in books)
            {
                context.OrderBooks.Add(new OrderBook
                {
                    OrderId = currentOrderId,
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
