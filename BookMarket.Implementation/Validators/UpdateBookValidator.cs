using BookMarket.Application.DataTransfer;
using BookMarket.DataAccess;
using BookMarket.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class UpdateBookValidator : AbstractValidator<Book>
    {
        private readonly BookMarketContext _context;
        public UpdateBookValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Length(1, 200)
                .WithMessage("Title must be between 1 and 200 characters long.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .Length(2, 500)
                .WithMessage("Description must be between 2 and 500 characters long.");
            RuleFor(x => x.Year)
                .Length(1, 4)
                .WithMessage("Year must be between 1 and 4 characters long.");
            RuleFor(x => x.Pages)
                .NotEmpty()
                .WithMessage("Pages is a required field.");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .Must(price => price > 0 && price < 10000)
                .WithMessage("Price is out of range.");
            RuleFor(x => x.WriterId)
                .Must(w => _context.Writers.Any(writer => writer.Id == w))
                .WithMessage("Given writer does not exist in the database.");
            RuleFor(x => x.PublisherId)
                .Must(p => _context.Publishers.Any(publ => publ.Id == p))
                .WithMessage("Given publisher does not exist in the database.");
            RuleFor(x => x.BookGenres)
                .NotEmpty()
                .WithMessage("No book genre defined.");
        }
    }
}
