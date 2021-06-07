using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using System.Net.Http;
using Entities.Models;
using System.Text.Json;
using System.Text;
using RemindMeOfSpecialBirthdays.Client.Contracts;

namespace RemindMeOfSpecialBirthdays.Client.HttpRepository
{
    public class NotificationInfoHttpRepo : INotificationInfoHttpRepo
    {
        private HttpClient _client;
        
        public NotificationInfoHttpRepo(HttpClient client)
        {
            _client = client;
        }

        //This method is used to send notication details to the server
        public async Task SubmitUserNoficationInfo(UserNotificationInfo notificationInfo)
        {
            string content = JsonSerializer.Serialize(notificationInfo);
            StringContent bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await _client.PutAsync("https://localhost:5031/api/account/submituserinfo", bodyContent);
            string putContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
