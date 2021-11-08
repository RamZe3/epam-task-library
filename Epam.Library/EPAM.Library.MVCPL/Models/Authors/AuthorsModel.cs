using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Models
{
    public class AuthorsModel
    {

        public List<SelectListItem> GetSelectListItems()
        {
            List<Author> authors = DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var author in authors)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = author.Name + " " + author.Surname,
                    Value = author.Id.ToString()
                };
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        public Author GetAuthorById(Guid id)
        {
            Author author = DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors().Find( x => x.Id == id);

            return author;
        }
    }
}