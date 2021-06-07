using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using RemindMeOfSpecialBirthdays.Client.Features;
using System.Net.Http;
using Entities.Models;
using System.Text.Json;

namespace RemindMeOfSpecialBirthdays.Client.HttpRepository
{
    //This class allows the client appication to be able to send requests for resources to the server application
    public class BirthdayMessagesHttpRepository : IBirthdayMessagesHttpRepositorycs
    {
        private PublicClient  _client;
        
        public BirthdayMessagesHttpRepository(PublicClient client)
        {
            _client = client;
        }

        //This method is used to send a GET request (for five random birhtday messages) to the server
        public async Task<List<BirthdayMessage>> GetFiveRandomBirthDayMessages()
        {
            HttpResponseMessage response = await _client.Client.GetAsync("https://localhost:5031/api/birthdaymessages");
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

           List<BirthdayMessage> fiveRandomMessages = JsonSerializer.Deserialize<List<BirthdayMessage>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return fiveRandomMessages;
        }
    }
}
