using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using RemindMeOfSpecialBirthdays.Client.Shared;
using Entities.Models;
using RemindMeOfSpecialBirthdays.Client.Contracts;

namespace RemindMeOfSpecialBirthdays.Client.Pages
{
    public partial class UpdateBirthday
    {
        private SuccessNotification successNotification;
        private Person person;

        [Inject]
        ISpecialBirthdaysHttpRepository SpecialBirthdaysHttprepo { get; set; }
        
        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            person = await SpecialBirthdaysHttprepo.GetPersonBirthdayAsync(Id);
        }

        private async Task Update()
        {
            await SpecialBirthdaysHttprepo.UpdatePersonBirthdayAsync(person);
            successNotification.Show();
        }
    }
}
