using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMessageRepository
    {
        Task<List<BirthdayMessage>> GetBirhtdayMessagesAsync();
    }
}
