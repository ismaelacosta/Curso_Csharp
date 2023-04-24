using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntroduccionAEFCore.Data.Entities;
using IntroduccionAEFCore.DTOs;

namespace IntroduccionAEFCore.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GenderCreationDTO, Gender>();
            CreateMap<ActorCreationDTO, Actor>();
            CreateMap<Actor, ActorDTO>();

            CreateMap<CommentaryCreationDTO, Commentary>();

            CreateMap<MovieCreationDTO, Movie>()
            .ForMember(ent => ent.Genders, dto => dto.MapFrom(campo => campo.Genders.Select(id => new Gender
            {
                Id = id
            }))); // Proyeccion 

            CreateMap<MovieActorCreationDTO, MovieActor>();

        }
    }
}