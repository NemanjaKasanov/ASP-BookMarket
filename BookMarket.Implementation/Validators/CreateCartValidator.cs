using BookMarket.DataAccess;
using BookMarket.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class CreateCartValidator : AbstractValidator<Cart>
    {
        private readonly BookMarketContext _context;
        public CreateCartValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is Required.")
                .Must(user_id => context.Users.Any(u => u.Id == user_id))
                .WithMessage("Given User does not exist in the database.");
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("BookId is Required.")
                .Must(book_id => context.Books.Any(b => b.Id == book_id))
                .WithMessage("Given Book does not exist in the database.");
            RuleFor(x => x.Quantity)
                 .NotEmpty()
                 .WithMessage("Quantity is a required field.")
                 .Must(quantity => quantity > 0 && quantity < 10)
                 .WithMessage("Quantity must be above 0.");
        }
    }
}
