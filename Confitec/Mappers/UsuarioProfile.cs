﻿using AutoMapper;
using Confitec.Dtos;
using Confitec.Models;

namespace Confitec.Mappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioCreateDto>();
            CreateMap<UsuarioCreateDto, Usuario>();
        }
    }
}
