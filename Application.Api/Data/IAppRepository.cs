using System.Collections.Generic;
using Application.Api.Models;

namespace Application.Api.Data
{
    public interface IAppRepository
    {
        IEnumerable<Person> GetPersons();
        Person GetPersonById(int personId);
    }
}