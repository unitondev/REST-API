using System.Collections.Generic;
using Application.Api.Data;
using Application.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IAppRepository _repository;

        public HomeController(IAppRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            var personsList = _repository.GetPersons();

            return Ok(personsList);
        }

        [HttpGet("{id}", Name = "GetPersonById")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var personItem = _repository.GetPersonById(id);

            return Ok(personItem);
        }
    }
}