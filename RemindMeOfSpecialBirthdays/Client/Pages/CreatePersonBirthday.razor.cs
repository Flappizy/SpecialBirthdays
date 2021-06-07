using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using RemindMeOfSpecialBirthdays.Client.Shared;
using Microsoft.AspNetCore.Components;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace RemindMeOfSpecialBirthdays.Client.Pages
{
    public partial class CreatePersonBirthday
    {
        private Person person = new Person();
        private SuccessNotification successNotification;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ISpecialBirthdaysHttpRepository specialBirthdaysHttpRepo { get; set; }

        public async Task Create()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            person.UserId = authState.User.Identity.Name;
            await specialBirthdaysHttpRepo.CreatePersonBirthdayAsync(person);
            successNotification.Show();
        }
    }
}
