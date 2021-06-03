using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.FullName, x => x.MapFrom(user => user.FirstName + " " + user.LastName));
            CreateMap<UserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UpdateUserDto>();
        }
    }
}
