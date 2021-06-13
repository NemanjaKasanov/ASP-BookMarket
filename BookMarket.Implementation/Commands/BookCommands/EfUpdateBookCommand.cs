using AutoMapper;
using BookMarket.Application.Commands.BookCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Implementation.Commands.BookCommands
{
    public class EfUpdateBookCommand : IUpdateBookCommand
    {
        public readonly BookMarketContext context;
        public readonly UpdateBookValidator validator;
        public readonly IMapper mapper;

        public EfUpdateBookCommand(BookMarketContext context, UpdateBookValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Update Book Command";

        public void Execute(Book dto)
        {
            var book = context.Books.Find(dto.Id);
            if (book == null) throw new EntityNotFoundException(dto.Id, typeof(Book));

            validator.ValidateAndThrow(dto);

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.Year = dto.Year;
            book.Price = dto.Price;
            book.Pages = dto.Pages;
            book.WriterId = dto.WriterId;
            book.PublisherId = dto.PublisherId;

            context.Database.ExecuteSqlRaw("DELETE FROM BookGenre WHERE BookId=" + dto.Id);
            foreach(var el in dto.BookGenres)
            {
                context.Database.ExecuteSqlRaw("INSERT INTO BookGenre (GenreId, BookId) VALUES (" + el.GenreId + ", " + dto.Id + ")");
            }

            context.SaveChanges();
        }
    }
}
