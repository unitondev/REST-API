using System.Collections.Generic;
using System.Linq;
using Application.Api.Models;

namespace Application.Api.Data
{
    public class InMemoryDbRepository : IAppRepository
    {
        private ApplicationDbContext _dbContext;

        public InMemoryDbRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Person> GetPersons()
        {
            return _dbContext.Persons.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _dbContext.Persons.FirstOrDefault(person => person.Id == id);
        }
    }
}