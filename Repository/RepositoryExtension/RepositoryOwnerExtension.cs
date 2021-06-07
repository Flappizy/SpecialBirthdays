using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.Models;

namespace Repository.RepositoryExtension
{
    //This class is used to create an extension method for the IQuerable class, i created a Search extension method, which i use when
    //a user wants to search for a particular person birthday
    public static class RepositoryOwnerExtension 
    {
        public static IQueryable<Person> Search(this IQueryable<Person> specialBirthdays, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return specialBirthdays;
            }

            string lowerCaseSearchTerm = searchTerm.ToLower().Trim();
            return specialBirthdays.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
