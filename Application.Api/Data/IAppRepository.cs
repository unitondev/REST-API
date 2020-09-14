using System.Collections.Generic;
using Application.Api.Models;

namespace Application.Api.Data
{
    public interface IAppRepository
    {
        bool SaveChanges();
        
        IEnumerable<Person> GetPersons();
        Person GetPersonById(int personId);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
    }
}