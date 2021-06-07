using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using RemindMeOfSpecialBirthdays.Client.Features;
using Entities.RequestFeatures;
using Entities.DTO;
using Microsoft.AspNetCore.Components;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace RemindMeOfSpecialBirthdays.Client.Pages
{
    //This class is used to represent the record of birthdays of a user
    public partial class Birthdays
    {
        public List<Person> ListOfPersonsSpecialBirthdays { get; set; } = new List<Person>();
        private PersonsBirhtdaysParameters requestSpecialBirthdaysParam = new PersonsBirhtdaysParameters();
        public MetaData PageMetaData { get; set; } = new MetaData();

        [Inject]
        public ISpecialBirthdaysHttpRepository SpecialBirthdaysHttpRepository { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetSpecialBirthdays();
        }

        private async Task DeletePerson(long id)
        {
            await SpecialBirthdaysHttpRepository.DeletePersonBirhtdayAsync(id);
            requestSpecialBirthdaysParam.PageNumber = 1;
            await GetSpecialBirthdays();
        }

        private async Task SelectedPage(int page)
        {
            requestSpecialBirthdaysParam.PageNumber = page;
            await GetSpecialBirthdays();
        }

        private async Task GetSpecialBirthdays()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            requestSpecialBirthdaysParam.UserId = authState.User.Identity.Name;
            PagingResponse<Person> pagingResponse = await SpecialBirthdaysHttpRepository.GetAllSpecialBirthDaysAsync(requestSpecialBirthdaysParam);
            ListOfPersonsSpecialBirthdays = pagingResponse.PersonsBirthdays;
            PageMetaData = pagingResponse.PageMetadata;
        }

        public async Task SearchChanged(string searchTerm)
        {
            requestSpecialBirthdaysParam.PageNumber = 1;
            requestSpecialBirthdaysParam.SearchTerm = searchTerm;
            await GetSpecialBirthdays();
        }
    }
}
