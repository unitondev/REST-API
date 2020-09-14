using System.Collections.Generic;
using Application.Api.Models;

namespace Application.Api.Data
{
    public class MockAppRepository : IAppRepository
    {
        public IEnumerable<Person> GetPersons()
        {
            var persons = new List<Person>
            {
                new Person {Id = 1, FirstName = "1Igor", Sex = "M", PrivateInformation = "+123912031"},
                new Person {Id = 2, FirstName = "2Igor", Sex = "M", PrivateInformation = "+123912031"},
                new Person {Id = 3, FirstName = "3Igor", Sex = "M", PrivateInformation = "+123912031"},
                new Person {Id = 4, FirstName = "4Igor", Sex = "M", PrivateInformation = "+123912031"}
            };

            return persons;
        }

        public Person GetPersonById(int personId)
        {
            return new Person{Id = 1, FirstName = "Igor", Sex = "M", PrivateInformation = "+123912031"};
        }
    }
}