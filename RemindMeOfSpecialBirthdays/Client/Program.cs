using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RemindMeOfSpecialBirthdays.Client.HttpRepository;
using RemindMeOfSpecialBirthdays.Client.Contracts;
using RemindMeOfSpecialBirthdays.Client.Features;
using RemindMeOfSpecialBirthdays.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace RemindMeOfSpecialBirthdays.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("SpecialBirthdays.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SpecialBirthdays.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddScoped<ISpecialBirthdaysHttpRepository, SpecialBirthdayHttpRepository>();
            builder.Services.AddScoped<IBirthdayMessagesHttpRepositorycs, BirthdayMessagesHttpRepository>();
            builder.Services.AddScoped<INotificationInfoHttpRepo, NotificationInfoHttpRepo>();
            builder.Services.AddScoped<Services.ClipboardService>();


            await builder.Build().RunAsync();
        }
    }
}
