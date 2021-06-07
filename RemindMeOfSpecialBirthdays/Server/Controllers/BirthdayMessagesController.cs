using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;
using Microsoft.AspNetCore.Authorization;

namespace RemindMeOfSpecialBirthdays.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthdayMessagesController : ControllerBase
    {
        private IMessageRepository _messageRepository;

        public BirthdayMessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiveRandomBirthdayMessages()
        {
            List<BirthdayMessage> fiveRandomBirthdayMessages = await _messageRepository.GetBirhtdayMessagesAsync();
            return Ok(fiveRandomBirthdayMessages);
        }
    }
}
