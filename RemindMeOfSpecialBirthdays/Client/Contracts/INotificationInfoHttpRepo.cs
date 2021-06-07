using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using System.Threading.Tasks;

namespace RemindMeOfSpecialBirthdays.Client.Contracts
{
    public interface INotificationInfoHttpRepo
    {
        Task SubmitUserNoficationInfo(UserNotificationInfo notificationInfo);
    }
}
