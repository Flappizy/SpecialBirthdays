using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemindMeOfSpecialBirthdays.Client.Features;
using Entities.Models;
using Entities.RequestFeatures;

namespace RemindMeOfSpecialBirthdays.Client.Contracts
{
    public interface ISpecialBirthdaysHttpRepository
    {
        Task<PagingResponse<Person>> GetAllSpecialBirthDaysAsync(PersonsBirhtdaysParameters personsBirhtdaysParameters);
        Task CreatePersonBirthdayAsync(Person person);
        Task UpdatePersonBirthdayAsync(Person person);
        Task DeletePersonBirhtdayAsync(long id);
        Task<Person> GetPersonBirthdayAsync(string personId);
    }
}
