using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epam.Library.Entities;
using EPAM.Library.MVCPL.ViewModels.Author;
using EPAM.Library.MVCPL.ViewModels.Book;

namespace EPAM.Library.MVCPL.Controllers
{
    //[Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
        // GET: Books

        public static Book bookBuffer;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateBookVM model)
        {

            //if (ModelState.IsValid)
            //{
            //    DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.AddPaper(AutoMapperConfig.Mapper.Map<Paper>(model));
            //    return RedirectToAction(nameof(Index), "Main");
            //}

            if (ModelState.IsValid)
            {
                bookBuffer = AutoMapperConfig.Mapper.Map<Book>(model);
                return RedirectToAction(nameof(Index), "Main");
                //return RedirectToAction(nameof(AddAuthors), new { CountOfAuthor = model.CountOfAuthor });
            }

            return View(model);
        }


        public ActionResult AddAuthors()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthors(List<CreateAuthorVM> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    Author author = AutoMapperConfig.Mapper.Map<Author>(item);
                    bookBuffer.Authors.Add(author);
                }
                DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.AddBook(bookBuffer);
                return RedirectToAction(nameof(Index), "Main");
            }
            return View(model);
        }
    }
}