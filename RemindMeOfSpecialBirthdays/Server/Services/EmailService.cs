using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemindMeOfSpecialBirthdays.Server.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using Contracts;
using Entities.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using RemindMeOfSpecialBirthdays.Server.Models;
using RemindMeOfSpecialBirthdays.Server.Data;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace RemindMeOfSpecialBirthdays.Server.Services
{
    //This class is used to send an email
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private RepositoryContext _repo;

        public EmailService(IOptions<EmailSettings> emailSettings, UserManager<ApplicationUser> userManager, RepositoryContext repositoryContext)
        {
            _emailSettings = emailSettings.Value;
            _userManager = userManager;
            _repo = repositoryContext;
        }

        //This class handles the sending of emails
        public async Task SendBirthdayReminderMailAsync(MailInfo mailInfo)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Template\\BirthdayReminderTemplate.html";
            string listOfPersonsBirthdays = AppendPersonsBirthdaysToHtmlBody(mailInfo.PersonsBirthdays);
            StreamReader read = new StreamReader(filePath);
            string mailText = read.ReadToEnd();
            read.Close();

            mailText = mailText.Replace("[User]", mailInfo.User);
            mailText = mailText.Replace("[numberOfdays]", mailInfo.NumberOfDays);
            mailText = mailText.Replace("[List Of Birhtdays]", listOfPersonsBirthdays);
            
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailInfo.RecieverMail));
            email.Subject = $"Reminder of Upcoming Birthdays for {mailInfo.User}";
            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;
            email.Body = builder.ToMessageBody();
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }

        //This method is used to add the list of birthdays to the template html that will be sent as an email
        private string AppendPersonsBirthdaysToHtmlBody(IEnumerable<Person> persons)
        {
            StringBuilder listofPersonsbirthdays = new StringBuilder();

            foreach (var person in persons)
            {
                listofPersonsbirthdays.Append($"<li>{person.Name} {person.BirthDay.ToShortDateString()}</li>");
            }
            return listofPersonsbirthdays.ToString();
        }

        public async Task NotifyThreeDaysToBirthday()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<Person> personsBirthday = await _repo.SpecialPersonsBirthDays.ToListAsync();
            MailInfo mailInfo = new MailInfo();

            if (users.Count != 0 && personsBirthday.Count != 0)
            {
                //This loop makes sure that for each user it tries to get the list of special birthdays that are coming up in
                //Three days time
                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = 0; j < personsBirthday.Count; j++)
                    {
                        if (users[i].UserName == personsBirthday[j].UserId)
                        {
                            //This used to check if the particular birthday has the same month as the present month and
                            //it also checks the difference between the birthday(day) and the present time day, if the difference is 3
                            //then it adds it to the list of birthdays we are trying to notify our user of
                            if ((personsBirthday[j].BirthDay.Month.CompareTo(DateTime.Now.Month) == 0) &&
                                (personsBirthday[j].BirthDay.Day - DateTime.Now.Day == 3))
                            {
                                mailInfo.PersonsBirthdays.Add(personsBirthday[j]);
                            }
                        }
                    }

                    if (mailInfo.PersonsBirthdays.Count != 0)
                    {
                        mailInfo.User = users[i].UserName;
                        mailInfo.RecieverMail = users[i].Email;
                        mailInfo.NumberOfDays = "in three days time";
                        await SendBirthdayReminderMailAsync(mailInfo);
                    }
                    mailInfo = new MailInfo();
                }
            }
        }

        public async Task NotifyOnDayOfBirthday()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<Person> personsBirthday = await _repo.SpecialPersonsBirthDays.ToListAsync();
            MailInfo mailInfo = new MailInfo();

            if (users.Count != 0 && personsBirthday.Count != 0)
            {
                //This loop makes sure that for each user it tries to get the list of special birthdays that are coming up in
                //Three days time
                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = 0; j < personsBirthday.Count; j++)
                    {
                        if (users[i].UserName == personsBirthday[j].UserId)
                        {
                            //This used to check if the particular birthday has the same month as the present month and
                            //it also checks the difference between the birthday(day) and the present time day, if the difference is 3
                            //then it adds it to the list of birthdays we are trying to notify our user of
                            if ((personsBirthday[j].BirthDay.Month.CompareTo(DateTime.Now.Month) == 0) &&
                                (personsBirthday[j].BirthDay.Day - DateTime.Now.Day == 0))
                            {
                                mailInfo.PersonsBirthdays.Add(personsBirthday[j]);
                            }
                        }
                    }

                    if (mailInfo.PersonsBirthdays.Count != 0)
                    {
                        mailInfo.User = users[i].UserName;
                        mailInfo.RecieverMail = users[i].Email;
                        mailInfo.NumberOfDays = "Today";
                        await SendBirthdayReminderMailAsync(mailInfo);
                    }
                    mailInfo = new MailInfo();
                }
            }                      
        }
    }
}
