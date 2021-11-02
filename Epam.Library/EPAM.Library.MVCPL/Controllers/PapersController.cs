using Epam.Library.Entities;
using EPAM.Library.MVCPL.ViewModels.Paper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
    public class PapersController : Controller
    {
        // GET: Papers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePaperVM model)
        {

            if (ModelState.IsValid)
            {
                DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.AddPaper(AutoMapperConfig.Mapper.Map<Paper>(model));
                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }
    }
}