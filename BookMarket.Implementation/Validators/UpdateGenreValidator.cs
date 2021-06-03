using BookMarket.Application.DataTransfer;
using BookMarket.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class UpdateGenreValidator : AbstractValidator<UpdateGenreDto>
    {
        private readonly BookMarketContext _context;
        public UpdateGenreValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required for genre.")
                .Length(2, 30)
                .WithMessage("Name must be between 2 and 30 characters long.")
                .Must(name => !_context.Genres.Any(x => x.Name == name))
                .WithMessage(g => $"Genre with name {g.Name} already exists in the database.");
        }
    }
}
