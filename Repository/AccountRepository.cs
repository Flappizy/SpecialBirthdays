using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Repository.RepositoryExtension;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    //This class allows the account owner to have access to the features the app provides, like add a new person birthday, delete a person birthday
    //update a birthday or even get all the account owner's stored special birthdays
    public class AccountRepository : IAccountOwnerRepository
    {
        private RepositoryContext _repositoryContext;

        public AccountRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<PagedList<Person>> GetAllSpecialBirthDaysAsync(PersonsBirhtdaysParameters personsBirhtdaysParameters)
        {
            //This is used a situation where the user has no record of special birthdays
            if (_repositoryContext.SpecialPersonsBirthDays.Count() == 0)
            {
                List<Person> birthdays = new List<Person>();
                return new PagedList<Person>(birthdays, 0, personsBirhtdaysParameters.PageSize, personsBirhtdaysParameters.PageNumber);
            }

            List<Person> personsBirthdays = await _repositoryContext.SpecialPersonsBirthDays.Search(personsBirhtdaysParameters.SearchTerm).
                Where(p => p.UserId.Equals(personsBirhtdaysParameters.UserId)).ToListAsync();
            return PagedList<Person>.ToPagedList(personsBirthdays, personsBirhtdaysParameters.PageSize, personsBirhtdaysParameters.PageNumber);
        }

        public async Task CreatePersonBirthdayAsync(Person person)
        {
            _repositoryContext.SpecialPersonsBirthDays.Add(person);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task UpdatePersonBirthdayAsync(Person person, Person dbPerson)
        {
            dbPerson.Name = person.Name;
            dbPerson.BirthDay = person.BirthDay;
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task DeletePersonBirhtdayAsync(Person person)
        {
            _repositoryContext.SpecialPersonsBirthDays.Remove(person);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task<Person> GetPersonByIdAsync(long PersonId)
        {
            Person person = await _repositoryContext.SpecialPersonsBirthDays.FirstOrDefaultAsync(p => p.PersonId == PersonId);
            return person;
        }
    }
}
