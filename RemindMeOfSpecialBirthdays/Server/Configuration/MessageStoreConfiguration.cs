using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace RemindMeOfSpecialBirthdays.Server.Configuration
{
    //This class allows me to be prepopulate the table, that stores the birthday messages 
    class MessageStoreConfiguration
    {
        public async static Task EnsurePopulated(IApplicationBuilder app)
        {
            //I get the application's database context from the application services
            RepositoryContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RepositoryContext>();
            
            //Check if there are any pending migrations, if there are any, i apply
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            //Checks if the table that holds the application birthday messages is empty 
            if (!context.BirthdayMessages.Any())
            {
                //This file contains the application's birthday messages
                string filePath = "MessageStore.txt";
                
                //Create a stream that will be used to read the text within the MessageStore.txt file
                StreamReader reader = new StreamReader(filePath);
                //Reads the text file line by line and store each line within the birthday messages table
                using (reader)
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        await context.BirthdayMessages.AddAsync(new BirthdayMessage {Message = line });
                        line = reader.ReadLine();
                    }
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}
