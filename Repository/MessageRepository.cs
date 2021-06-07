using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.DTO;
using Entities.Models;
using Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    //This class allows the users to access the birthmessages repository
    public class MessageRepository : IMessageRepository
    {
        private readonly RepositoryContext  _repositoryContext;

        public MessageRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        
        //This method allow users to get 5 random messages at a time from the application's birthday messages repository
        public async Task<List<BirthdayMessage>> GetBirhtdayMessagesAsync()
        {
            Random random = new Random();
            List<BirthdayMessage> birthdayMessages = new List<BirthdayMessage>();
            for (int i = 0; i < 5; i++)
            {
                int randomNumber = random.Next(1, 222);
                BirthdayMessage randomBirhtdayMessage =  await _repositoryContext.BirthdayMessages.
                    FirstOrDefaultAsync(bdayMess => bdayMess.Id == randomNumber);
               birthdayMessages.Add(randomBirhtdayMessage);
            }
            return birthdayMessages;
        }
    }
}
