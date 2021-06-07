using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities
{
    public class RepositoryContext :DbContext
    { 
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }
        
        public DbSet<Person> SpecialPersonsBirthDays { get; set; }
        public DbSet<BirthdayMessage> BirthdayMessages { get; set; }
    }
}
