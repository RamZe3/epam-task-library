using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.Author;
using EPAM.Library.MVCPL.ViewModels.Patent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
    [ExceptionLogger]
    [AuthorizeLogger(Roles = "admin, librarian")]
    public class PatentsController : Controller
    {
        // GET: Patents
        public static Patent patentBuffer;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePatentVM model)
        {

            if (ModelState.IsValid)
            {
                AuthorsModel authorsModel = new AuthorsModel();
                Patent patent = AutoMapperConfig.Mapper.Map<Patent>(model);
                foreach (var authorId in model.AuthorsId)
                {
                    patent.Inventors.Add(authorsModel.GetAuthorById(new Guid(authorId)));
                }
                DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.AddPatent(patent);
                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }

        public ActionResult Edit(CreatePatentVM model, int i = 0)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CreatePatentVM model)
        {

            if (ModelState.IsValid)
            {
                AuthorsModel authorsModel = new AuthorsModel();
                Patent patent = AutoMapperConfig.Mapper.Map<Patent>(model);
                foreach (var authorId in model.AuthorsId)
                {
                    patent.Inventors.Add(authorsModel.GetAuthorById(new Guid(authorId)));
                }
                DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.UpdatePatent(patent);
                return RedirectToAction(nameof(Index), "Main");
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
                    patentBuffer.Inventors.Add(author);
                }
                DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.AddPatent(patentBuffer);
                return RedirectToAction(nameof(Index), "Main");
            }
            return View(model);
        }
    }
}