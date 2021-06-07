using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace RemindMeOfSpecialBirthdays.Client.Contracts
{
    public interface IBirthdayMessagesHttpRepositorycs
    {
        Task<List<BirthdayMessage>> GetFiveRandomBirthDayMessages();
    }
}
