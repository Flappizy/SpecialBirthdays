using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Entities.RequestFeatures;
using RemindMeOfSpecialBirthdays.Client.Features;

namespace RemindMeOfSpecialBirthdays.Client.Components
{
    //This is the class that handles pagination
    public partial class Pagination
    {
        [Parameter]
        public MetaData MetaData { get; set; }

        [Parameter]
        public int Spread { get; set; }

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        //This method creates the within the special birthdays page links
        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();

            //What i did here is adding a link that reads "previous", if this link is clicked it loads the previous page
            //The "MetaData.CurrentPage - 1" parameter makes sure that when the page loads for the first time, the previous link can not go further behind when clicked
            //The MetaData.HasPrevious parameter is used to determine if the "previous" link still has a previous page it can go to
            //The "Previous" parameter is the link text
            _links.Add(new PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "Previous"));

            //This loop is used to build the link from "link (1) to link (total pages)"
            for (int i = 1; i <= MetaData.TotalPages; i++)
            {
                //This used  to determine the links that show up on the left and right side of current page link number when you on a particular page
                //For example imagine we have 50 page links, we do not want to show all the 50 on all pages, so what the below code does is
                //it checks what page you are on, and displays the appropriate links that can be navigated within that page
                if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }
            
            //This is used to add the link that reads "next"
            //The "MetaData.CurrentPage + 1" parameter makes sure that when you click the link, it goes to the next page
            //The "MetaData.HasNext" parameter is used to check if the current page has a next page
            //The "Next" parameter is the link text
            _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, "Next"));
        }

        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;

            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
