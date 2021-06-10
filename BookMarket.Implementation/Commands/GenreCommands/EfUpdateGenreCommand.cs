using AutoMapper;
using BookMarket.Application.Commands.GenreCommands;
using BookMarket.Application.DataTransfer;
using BookMarket.Application.Exceptions;
using BookMarket.DataAccess;
using BookMarket.Domain;
using BookMarket.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Commands.GenreCommands
{
    public class EfUpdateGenreCommand : IUpdateGenreCommand
    {
        public readonly BookMarketContext context;
        public readonly UpdateGenreValidator validator;
        public readonly IMapper mapper;

        public EfUpdateGenreCommand(BookMarketContext context, UpdateGenreValidator validator, IMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Update Genre";

        public void Execute(UpdateGenreDto dto)
        {
            var genre = context.Genres.Find(dto.Id);
            if (genre == null) throw new EntityNotFoundException(dto.Id, typeof(Genre));

            validator.ValidateAndThrow(dto);

            mapper.Map(dto, genre);
            context.SaveChanges();
        }
    }
}
