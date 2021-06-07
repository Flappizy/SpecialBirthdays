using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RemindMeOfSpecialBirthdays.Server.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace RemindMeOfSpecialBirthdays.Server.Data
{
    public class NotificationInfo
    {
        private ApplicationDbContext _dbContext;

        public NotificationInfo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SumbitUserInfo(ApplicationUser user, UserNotificationInfo notificationInfo)
        {
            user.Email = notificationInfo.MailAddress != null ? notificationInfo.MailAddress : user.Email;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
