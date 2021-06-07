using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Entities.Models;
using System.Linq;

namespace Entities.Models
{
    //This class is used to model an invitation sent
    public class MailInfo
    {
        public string RecieverMail { get; set; }
        public string NumberOfDays { get; set; }
        public string User { get; set; }
        public List<Person> PersonsBirthdays = new List<Person>();
    }
}
