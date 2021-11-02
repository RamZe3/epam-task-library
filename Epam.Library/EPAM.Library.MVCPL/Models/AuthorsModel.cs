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
        public List<Author> authors => DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors();

        public List<SelectListItem> GetSelectListItems()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var author in authors)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = author.Name + " " + author.Surname,
                    Value = author.Id.ToString(),
                    Selected = true
                };
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
    }
}