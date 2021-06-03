using BookMarket.DataAccess;
using BookMarket.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Validators
{
    public class CreateUserValidator : AbstractValidator<User>
    {
        private readonly BookMarketContext _context;
        public CreateUserValidator(BookMarketContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FistName is required.")
                .Length(2, 40)
                .WithMessage("FirstName must be between 2 and 40 characters.")
                .Matches(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")
                .WithMessage("Name is in a wrong format.");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .Length(2, 40)
                .WithMessage("LastName must be between 2 and 40 characters.")
                .Matches(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")
                .WithMessage("Last Name is in a wrong format.");
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Length(2, 40)
                .WithMessage("Username must be between 2 and 40 characters.")
                .Must(username => !_context.Users.Any(usr => usr.Username == username))
                .WithMessage(user => $"User with username {user.Username} already exists.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Wrong email format.")
                .Must(email => !_context.Users.Any(usr => usr.Email == email))
                .WithMessage(email => $"User with username {email.Email} already exists.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$")
                .WithMessage("Password must include at least one lower case letter, one capital letter and at least one number and be between 6 and 30 characters.");
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
            RuleFor(x => x.Role)
                .Must(role => role == 0)
                .WithMessage("Role must be set to 0 -> User.");
        }
    }
}
