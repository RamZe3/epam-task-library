using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
    [ExceptionLogger]
    [AuthorizeLogger(Roles = "admin, librarian")]
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult GetAuthors()
        {
            return PartialView("_TempAuthorPartial");
        }

        public ActionResult AddAuthor(CreateAuthorVM model)
        {
            Author author = AutoMapperConfig.Mapper.Map<Author>(model);
            DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.AddAuthorWithoutTran(author);
            return PartialView("_TempAuthorPartial");
        }

        public ActionResult AddAuthorToDB()
        {

            return PartialView("_TempAddAuthorPartial", new CreateAuthorVM());
        }
    }
}