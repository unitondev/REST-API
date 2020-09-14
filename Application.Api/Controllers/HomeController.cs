using System.Collections.Generic;
using Application.Api.Data;
using Application.Api.Dtos;
using Application.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IAppRepository _repository;
        private IMapper _mapper;

        public HomeController(IAppRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<PersonViewDto>> GetPersons()
        {
            var personsItems = _repository.GetPersons();

            return Ok(_mapper.Map<IEnumerable<PersonViewDto>>(personsItems));
        }

        [HttpGet("{id}", Name = "GetPersonById")]
        public ActionResult<PersonViewDto> GetPersonById(int id)
        {
            var personItem = _repository.GetPersonById(id);

            if (personItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PersonViewDto>(personItem));
        }
        
        [HttpPost]
        public ActionResult<PersonViewDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            var personModel = _mapper.Map<Person>(personCreateDto);
            _repository.CreatePerson(personModel);
            _repository.SaveChanges();

            var personView = _mapper.Map<PersonViewDto>(personModel);
            
            return CreatedAtRoute(nameof(GetPersonById), new {Id = personView.Id}, personView);
        }
    }
}