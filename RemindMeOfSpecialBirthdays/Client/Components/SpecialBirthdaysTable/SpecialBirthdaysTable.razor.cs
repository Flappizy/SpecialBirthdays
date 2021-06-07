using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Entities.Models;
using System.IO;

namespace RemindMeOfSpecialBirthdays.Client.Components.SpecialBirthdaysTable
{
    //This class is used to represent all the birthdays within account
    public partial class SpecialBirthdaysTable
    {
        [Parameter]
        public List<Person> SpecialBirthdays { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<long> OnDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        //This method redirects you to the page that handles updating a birthday
        private void RedirectToUpdate(long id)
        {
            var url = Path.Combine("/updatebirthday/", id.ToString());
            NavigationManager.NavigateTo(url);
        }


        //This method handles the process of deleting a person's birthday
        private async Task Delete(long id)
        {
            Person person = SpecialBirthdays.FirstOrDefault(p => p.PersonId.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {person.Name} Birthday?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
