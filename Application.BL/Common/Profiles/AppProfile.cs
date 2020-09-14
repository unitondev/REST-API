using Application.BL.Dtos.PersonDtos;
using Application.Domain.Models;
using AutoMapper;

namespace Application.BL.Common.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Person, PersonViewDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonUpdateDto>();
        }
    }
}