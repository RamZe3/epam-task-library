using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.Paper;
using EPAM.Library.MVCPL.ViewModels.PaperIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
    [ExceptionLogger]
    [AuthorizeLogger(Roles = "admin, librarian")]
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

        [HttpGet]
        public ActionResult Edit(EditPaperVM model, int i=0)
        {

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditPaperVM model)
        {
            
            if (ModelState.IsValid)
            {
                DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.UpdatePaper(AutoMapperConfig.Mapper.Map<Paper>(model));
                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CreatePaperIssueRow(PaperIssue paperIssue)
        {
            DisplayPaperIssueVM displayPaperIssueVM = AutoMapperConfig.Mapper.Map<DisplayPaperIssueVM>(paperIssue);

            return PartialView("_RowPaperIssue", displayPaperIssueVM);
        }

        [AllowAnonymous]
        public ActionResult PICard(Guid id)
        {
            PaperIssue paperIssue = new PaperIssueLogic().GetPaperIssueById(id);
            DisplayCardPaperIssueVM displayCardPaperIssueVM = AutoMapperConfig.Mapper.Map<DisplayCardPaperIssueVM>(paperIssue);
            return View(displayCardPaperIssueVM);
        }

        public ActionResult CreatePaperIssue()
        {
            CreatePaperIssueVM createPaperIssueVM = new CreatePaperIssueVM();
            createPaperIssueVM.PapersList = new PaperIssueLogic().GetPapersSelectListItems();

            return View(createPaperIssueVM);
        }

        [HttpPost]
        public ActionResult CreatePaperIssue(CreatePaperIssueVM model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Paper> papers = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().OfType<Paper>();
                Paper paper = papers.ToList().Find(x => x.ISSN == model.ISSN);
                paper.Id = Guid.NewGuid();
                paper.Number = model.Number;
                paper.Date = model.Date;
                paper.NumberOfPages = model.NumberOfPages;
                DependenciesResolverConfig.DependenciesResolver.papersLogicWithRoles.AddPaper(paper);
                return RedirectToAction("Index", "Main");
            }

            return View(model);
        }


    }
}