using BookMarket.Application.DataTransfer;
using BookMarket.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly BookMarketContext _context;
        public UpdateUserValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .Length(2, 50)
                .WithMessage("Address must be between 2 and 50 characters.");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Length(6, 22)
                .WithMessage("Phone number is in an invalid format.");
        }
    }
}
