using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using System.ComponentModel.DataAnnotations;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using Microsoft.AspNetCore.Components;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using RemindMeOfSpecialBirthdays.Client.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace RemindMeOfSpecialBirthdays.Client.Pages
{
    public partial class Notification
    {
        private UserNotificationInfo userInfo = new UserNotificationInfo();
        private SuccessNotification successNotification;
        private EmptyFieldValueNotification failedNotication;
        private InvalidPhoneNumber invalidPhoneNumber;

        [Inject]
        public INotificationInfoHttpRepo NotificationInfoHttpRepo { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private async Task SumbitNotificationInfo()
        {
            if (userInfo.MailAddress == null)
            {
                failedNotication.Show();
            }

            else
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                userInfo.UserId = authState.User.Identity.Name;
                await NotificationInfoHttpRepo.SubmitUserNoficationInfo(userInfo);
                successNotification.Show();
            }
        }
    }
}
