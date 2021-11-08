using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.Author;
using EPAM.Library.MVCPL.ViewModels.Book;

namespace EPAM.Library.MVCPL.Controllers
{
    //[Authorize(Roles = "admin")]
    [AuthorizeLogger(Roles = "admin, librarian")]
    public class BooksController : Controller
    {
        // GET: Books

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionLogger("action with admin rights")]
        public ActionResult Create(CreateBookVM model)
        {
            if (ModelState.IsValid)
            {
                AuthorsModel authorsModel = new AuthorsModel();
                Book book = AutoMapperConfig.Mapper.Map<Book>(model);
                book.Id = Guid.NewGuid();
                foreach (var authorId in model.AuthorsId)
                {
                    book.Authors.Add(authorsModel.GetAuthorById(new Guid(authorId)));
                }
                DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.AddBook(book);
                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(CreateBookVM model, int i = 0)
        {
            return View(model);
        }

        [HttpPost]
        [ActionLogger("action with admin rights")]
        public ActionResult Edit(CreateBookVM model)
        {

            if (ModelState.IsValid)
            {
                AuthorsModel authorsModel = new AuthorsModel();
                Book book = AutoMapperConfig.Mapper.Map<Book>(model);
                foreach (var authorId in model.AuthorsId)
                {
                    book.Authors.Add(authorsModel.GetAuthorById(new Guid(authorId)));
                }
                DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.UpdateBook(book);
                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }

        //public ActionResult AddAuthors()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddAuthors(List<CreateAuthorVM> model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var item in model)
        //        {
        //            Author author = AutoMapperConfig.Mapper.Map<Author>(item);
        //            bookBuffer.Authors.Add(author);
        //        }
        //        DependenciesResolverConfig.DependenciesResolver.booksLogicWithRoles.AddBook(bookBuffer);
        //        return RedirectToAction(nameof(Index), "Main");
        //    }
        //    return View(model);
        //}
    }
}