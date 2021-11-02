using Epam.Library.Entities;
using EPAM.Library.MVCPL.ViewModels.Author;
using EPAM.Library.MVCPL.ViewModels.Patent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
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

            //if (ModelState.IsValid)
            //{
            //    DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.AddPaper(AutoMapperConfig.Mapper.Map<Paper>(model));
            //    return RedirectToAction(nameof(Index), "Main");
            //}

            if (ModelState.IsValid)
            {
                patentBuffer = AutoMapperConfig.Mapper.Map<Patent>(model);
                return RedirectToAction(nameof(AddAuthors), new { CountOfAuthor = model.CountOfAuthor });
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