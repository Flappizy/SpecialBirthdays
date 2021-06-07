using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountOwnerRepository
    {
        Task<PagedList<Person>> GetAllSpecialBirthDaysAsync(PersonsBirhtdaysParameters personsBirhtdaysParameters);
        Task CreatePersonBirthdayAsync(Person person);
        Task UpdatePersonBirthdayAsync(Person person, Person dbPerson);
        Task DeletePersonBirhtdayAsync(Person person);
        Task<Person> GetPersonByIdAsync(long PersonId);
    }
}
