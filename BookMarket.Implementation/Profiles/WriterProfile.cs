using AutoMapper;
using BookMarket.Application.DataTransfer;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Profiles
{
    public class WriterProfile : Profile
    {
        public WriterProfile()
        {
            CreateMap<Writer, WriterDto>();
            CreateMap<WriterDto, Writer>();
            CreateMap<Writer, UpdateWriterDto>();
            CreateMap<UpdateWriterDto, Writer>();
        }
    }
}
