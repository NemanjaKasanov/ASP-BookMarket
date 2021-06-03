using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, UpdateGenreDto>();
            CreateMap<UpdateGenreDto, Genre>();
        }
    }
}
