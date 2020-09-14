using System.Collections.Generic;
using Application.Api.Data;
using Application.Api.Dtos;
using Application.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        
        /// <summary>
        /// Получить модели
        /// </summary>
        /// <response code="200">Модели найдены</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Модели не найдены</response>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PersonViewDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<PersonViewDto>> GetPersons()
        {
            var personsItems = _repository.GetPersons();

            if (personsItems == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<IEnumerable<PersonViewDto>>(personsItems));
        }

        /// <summary>
        /// Получить модель по personId
        /// </summary>
        /// <param name="personId">Id модели</param>
        /// <response code="200">Модель найдена</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Модель не найдена</response>
        [HttpGet("{personId}", Name = "GetPersonById")]
        [ProducesResponseType(typeof(PersonViewDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<PersonViewDto> GetPersonById(int personId)
        {
            var personItem = _repository.GetPersonById(personId);

            if (personItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PersonViewDto>(personItem));
        }
        
        /// <summary>
        /// Создать модель
        /// </summary>
        /// <param name="personCreateDto">Создаваемая модель</param>
        /// <response code="201">Модель успешно создана</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="405">Метод запроса известен, но отключен и не может быть использован</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<PersonViewDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            var personModel = _mapper.Map<Person>(personCreateDto);
            _repository.CreatePerson(personModel);
            _repository.SaveChanges();

            var personView = _mapper.Map<PersonViewDto>(personModel);
            
            return CreatedAtRoute(nameof(GetPersonById), new {Id = personView.Id}, personView);
        }
        
        /// <summary>
        /// Заменить модель
        /// </summary>
        /// <param name="personId">Id модели</param>
        /// <param name="personUpdateDto">Новая модель</param>
        /// <response code="204">Модель заменена</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Модель не найдена</response>
        [HttpPut("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        
        /// <summary>
        /// Редактировать модель
        /// </summary>
        /// <param name="personId">Id модели</param>
        /// <param name="jsonPatchDocument">Json с операцией, путем и новым значением</param>
        /// <response code="204">Модель отредактирована</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Модель не найдена</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [HttpPatch("{personId}")]
        public ActionResult PatchPerson(int personId, JsonPatchDocument<PersonUpdateDto> jsonPatchDocument)
        {
            var personModelFromRepos = _repository.GetPersonById(personId);
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
        
        /// <summary>
        /// Удалить модель
        /// </summary>
        /// <param name="personId">Id модели</param>
        /// <response code="204">Модель удалена</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Модель не найдена</response>
        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeletePerson(int personId)
        {
            var personModelFromRepos = _repository.GetPersonById(personId);
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