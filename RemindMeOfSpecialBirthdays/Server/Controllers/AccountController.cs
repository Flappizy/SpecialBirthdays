using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Repository;
using Contracts;
using Entities.RequestFeatures;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using RemindMeOfSpecialBirthdays.Server.Data;
using RemindMeOfSpecialBirthdays.Server.Models;

namespace RemindMeOfSpecialBirthdays.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountOwnerRepository _accountRepository;
        private RepositoryContext _repositoryContext;
        private NotificationInfo _notificationInfo;
        private UserManager<ApplicationUser> _userManager;
     
        public AccountController(IAccountOwnerRepository accountRepository, RepositoryContext repositoryContext, NotificationInfo notificationInfo,
            UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _repositoryContext = repositoryContext;
            _notificationInfo = notificationInfo;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddBirthDay([FromBody]Person person)
        {
            if (person == null || !ModelState.IsValid)
            {
                return BadRequest(new PersonBirthdayResponseDto { ErrorMessage = "I can not keep track of a person with an invalid details, please" +"put in a valid details" });
            }
            await _accountRepository.CreatePersonBirthdayAsync(person);
            return Created("", person);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialBirthdays([FromQuery]PersonsBirhtdaysParameters personsBirhtdaysParameters)
        {            
            PagedList<Person> specialBirthdays = await _accountRepository.GetAllSpecialBirthDaysAsync(personsBirhtdaysParameters);
            Response.Headers.Add("Pagination", JsonSerializer.Serialize(specialBirthdays.MetaData));
            return Ok(specialBirthdays);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonBirthday([FromBody] Person person,  long id)
        {
            if (person == null || !ModelState.IsValid)
            {
                return BadRequest(new PersonBirthdayResponseDto {ErrorMessage = "You can not update a person's birthday with invalid" 
                    + "details"});
            }
                       
            Person dbPerson = await _accountRepository.GetPersonByIdAsync(id);
            if (dbPerson == null)
            {
                return NotFound();
            }
            await _accountRepository.UpdatePersonBirthdayAsync(person, dbPerson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonBirthdayFromList(long id)
        {
           Person person = await _accountRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound(new PersonBirthdayResponseDto { ErrorMessage = "Person with the specified birthday can not be found" });
            }
            await _accountRepository.DeletePersonBirhtdayAsync(person);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonBirthdayAsync(long id)
        {
            var person = await _accountRepository.GetPersonByIdAsync(id);
            return Ok(person);
        }

        [HttpPut("submituserinfo")]
        public async Task<IActionResult> SubmitUserNotificationInfo([FromBody]UserNotificationInfo notificationInfo)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(notificationInfo.UserId);

            if (user == null)
            {
                return NotFound();
            }

            if (notificationInfo == null)
            {
                return BadRequest();
            }
            
            await _notificationInfo.SumbitUserInfo(user, notificationInfo);
            return NoContent();
        }
    }
}
