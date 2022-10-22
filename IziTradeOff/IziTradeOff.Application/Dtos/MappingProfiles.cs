using AutoMapper;
using IziTradeOff.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Application.Dtos
{
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<Traduccion, TraduccionDto>().ReverseMap();
        }
    }
}
