using Application.Api.Dtos;
using Application.Api.Models;
using AutoMapper;

namespace Application.Api.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Person, PersonViewDto>();
        }
    }
}