using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using RemindMeOfSpecialBirthdays.Client.Features;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Text.Encodings;
using System.Text;
using System.IO;

namespace RemindMeOfSpecialBirthdays.Client.HttpRepository
{
    //This class is used for sending request to the server application
    public class SpecialBirthdayHttpRepository : ISpecialBirthdaysHttpRepository
    {
        private HttpClient _client;

        public SpecialBirthdayHttpRepository(HttpClient client)
        {
            _client = client;
        }

        //This method is used for getting the special birhtdays that is related to the user account, six birthdays at a time
        public async Task<PagingResponse<Person>> GetAllSpecialBirthDaysAsync(PersonsBirhtdaysParameters personsBirhtdaysParameters)
        {
            //This is used for setting up the query parameters that will be sent along with the server's url
            Dictionary<string, string> queryStringParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = personsBirhtdaysParameters.PageNumber.ToString() ?? 1.ToString(),
                ["searchTerm"] = personsBirhtdaysParameters.SearchTerm ?? "",
                ["orderBy"] = personsBirhtdaysParameters.OrderBy ?? "name",
                ["UserId"] = personsBirhtdaysParameters.UserId
            };

            //This variable contains the response we get from the server
            HttpResponseMessage response = await _client.GetAsync(QueryHelpers.AddQueryString("https://localhost:5031/api/account", queryStringParameters));
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            PagingResponse<Person> pagingResponse = new PagingResponse<Person>()
            {
                PersonsBirthdays = JsonSerializer.Deserialize<List<Person>>(content, new JsonSerializerOptions {PropertyNameCaseInsensitive = true }),
                PageMetadata = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("Pagination").First(), new JsonSerializerOptions {PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task CreatePersonBirthdayAsync(Person person)
        {
            string content = JsonSerializer.Serialize(person);
            StringContent bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("https://localhost:5031/api/account", bodyContent);
            string postContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdatePersonBirthdayAsync(Person person)
        {
            string content = JsonSerializer.Serialize(person);
            StringContent bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            string url = Path.Combine("https://localhost:5031/api/account", person.PersonId.ToString());
           
            HttpResponseMessage response = await _client.PutAsync(url, bodyContent);
            string putContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }

        public async Task DeletePersonBirhtdayAsync(long id)
        {
            string url = Path.Combine("https://localhost:5031/api/account", id.ToString());
            
            HttpResponseMessage response = await _client.DeleteAsync(url);
            string deleteContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<Person> GetPersonBirthdayAsync(string personId)
        {
            string url = Path.Combine("https://localhost:5031/api/account/", personId);
            HttpResponseMessage response = await _client.GetAsync(url);
            string getContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }

            Person person = JsonSerializer.Deserialize<Person>(getContent, new JsonSerializerOptions {PropertyNameCaseInsensitive = true });
            return person;
        }
    }
}
