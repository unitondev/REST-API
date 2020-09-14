using System.Collections.Generic;
using Application.Domain.Models;

namespace Application.BL.Common.Interfaces
{
    public interface IAppRepository
    {
        bool SaveChanges();
        
        IEnumerable<Person> GetPersons();
        Person GetPersonById(int personId);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}