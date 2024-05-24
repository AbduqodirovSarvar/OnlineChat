﻿using AutoMapper;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Role, y => y.MapFrom(z => z.Role))
                .ReverseMap();

            CreateMap<UserRole, EnumViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => ((int)z)))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.ToString()))
                .ReverseMap();
        }
    }
}
