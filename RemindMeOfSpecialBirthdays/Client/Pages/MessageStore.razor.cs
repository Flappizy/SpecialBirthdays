using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using System.IO;

namespace RemindMeOfSpecialBirthdays.Client.Pages
{
    //This class is used to send a request to the get birthday messages
    public partial class MessageStore
    {
        public List<BirthdayMessage> BirthdayMessages { get; set; } = new List<BirthdayMessage>();

        [Inject]
        public IBirthdayMessagesHttpRepositorycs BirthdayMessagesHttpRepo { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string BdayMssg { get; set; }


        protected async override Task OnInitializedAsync()
        {
            BirthdayMessages = await BirthdayMessagesHttpRepo.GetFiveRandomBirthDayMessages();
        }

        private async Task GetBirthdayMessages()
        {
            BirthdayMessages = await BirthdayMessagesHttpRepo.GetFiveRandomBirthDayMessages();
        }
    }
}
