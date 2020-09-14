using System;
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
        
        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
        
        public IEnumerable<Person> GetPersons()
        {
            return _dbContext.Persons.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _dbContext.Persons.FirstOrDefault(person => person.Id == id);
        }

        public void CreatePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            
            _dbContext.Persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            //Empty
        }
        
        public void DeletePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _dbContext.Persons.Remove(person);
        }
    }
}