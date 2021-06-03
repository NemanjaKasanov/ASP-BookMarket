using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<Publisher, UpdatePublisherDto>();
            CreateMap<UpdatePublisherDto, Publisher>();
        }
    }
}
