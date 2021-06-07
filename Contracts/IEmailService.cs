using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEmailService
    {
        Task SendBirthdayReminderMailAsync(MailInfo mailInfo);
        Task NotifyThreeDaysToBirthday();
        Task NotifyOnDayOfBirthday();
    }
}
