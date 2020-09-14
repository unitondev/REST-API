using Application.BL.Person;
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