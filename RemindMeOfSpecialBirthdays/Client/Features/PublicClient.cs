using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace RemindMeOfSpecialBirthdays.Client.Features
{
    //This class allows you access server resources that does not require authentication 
    public class PublicClient
    {
        public HttpClient Client { get; }

        public PublicClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }
}
