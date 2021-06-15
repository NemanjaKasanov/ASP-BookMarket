using BookMarket.DataAccess;
using BookMarket.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<Order>
    {
        private readonly BookMarketContext _context;
        public CreateOrderValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage("Given User doesn't exist in the database.");
        }
    }
}
