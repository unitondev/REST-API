using System.Collections.Generic;
using Application.Api.Data;
using Application.Api.Dtos;
using Application.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        
        [HttpPut("{personId}")]
        public ActionResult UpdatePerson(int personId, PersonUpdateDto personUpdateDto)
        {
            //personModelFromRepo - the object to be replaced. Type == Person.
            //personUpdateDto - new object. Map to Person.
            var personModelFromRepos = _repository.GetPersonById(personId);
            if (personModelFromRepos == null)
            {
                return NotFound();
            }
            
            //updated personModelFromRepos, changes are being tracked via dbContext
            _mapper.Map(personUpdateDto, personModelFromRepos);
            
            //for maintaining 
            _repository.UpdatePerson(personModelFromRepos);
            
            _repository.SaveChanges();
            
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public ActionResult PatchPerson(int id, JsonPatchDocument<PersonUpdateDto> jsonPatchDocument)
        {
            var personModelFromRepos = _repository.GetPersonById(id);
            if (personModelFromRepos == null)
            {
                return NotFound();
            }

            var personToPatch = _mapper.Map<PersonUpdateDto>(personModelFromRepos);
            
            jsonPatchDocument.ApplyTo(personToPatch, ModelState);
            
            if (!TryValidateModel(personToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            _mapper.Map(personToPatch, personModelFromRepos);
            
            //for maintaining 
            _repository.UpdatePerson(personModelFromRepos);
            
            _repository.SaveChanges();
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            var personModelFromRepos = _repository.GetPersonById(id);
            if (personModelFromRepos == null)
            {
                return NotFound();
            }
            
            _repository.DeletePerson(personModelFromRepos);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}