using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Logic.SearchFilters
{
    public class AuthorSearchFilter
    {
        public string Name { get; set; }

        public List<Author> Filter(List<Author> resources)
        {
            if (Name == null)
            {
                return resources;
            }

            return resources.FindAll(a => NameFilter(a));
        }

        private bool NameFilter(Author author)
        {
            return author.Name.Contains(Name) ? true : false;
        }
    }
}