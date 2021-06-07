using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    //This class is used to model a Person
    public class Person
    {
        public long PersonId { get; set; }
        
        [Required(ErrorMessage ="The name of the person you want to add is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="I can not keep track of the person's birthday if you don't give me the date")]
        public DateTime BirthDay { get; set; } 

        public string UserId { get; set; }
    }
}
