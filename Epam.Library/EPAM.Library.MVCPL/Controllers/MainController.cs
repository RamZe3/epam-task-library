using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.Book;
using EPAM.Library.MVCPL.ViewModels.Paper;
using EPAM.Library.MVCPL.ViewModels.Patent;
using EPAM.Library.MVCPL.ViewModels.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Controllers
{
    [ExceptionLogger]
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLibrary(int page = 1)
        {
            DisplayResourceVM displayResourceVM = new DisplayResourceVM();

            int pageSize = 1;

            displayResourceVM.Resources = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary()
                .Skip((page-1)*pageSize).Take(pageSize).ToList();

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Count };

            displayResourceVM.PageInfo = pageInfo;

            return View(displayResourceVM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetLibrary(DisplayResourceVM displayResourceVM, int page = 1)
        {

            int pageSize = 1;

            displayResourceVM.Resources = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.FindResourcesByName(displayResourceVM.NametoSearch)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = displayResourceVM.Resources.Count };

            displayResourceVM.PageInfo = pageInfo;

            return View(displayResourceVM);
        }

        [ChildActionOnly]
        public ActionResult CreateRow(InformationResource resource)
        {
            if (resource is Book)
            {

                Book book = (Book)resource;
                return PartialView("Table/_RowBook", book);
            }
            else if (resource is Paper)
            {
                Paper paper = (Paper)resource;
                return PartialView("Table/_RowPaper", paper);
            }
            else if (resource is Patent)
            {
                Patent patent = (Patent)resource;
                return PartialView("Table/_RowPatent", patent);
            }
            return View();
        }

        public ActionResult Card(Guid id)
        {
            
            return View(id);
        }

        [ChildActionOnly]
        public ActionResult CreateCard(Guid id)
        {
            InformationResource informationResource = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(IR => IR.Id == id);
            if (informationResource is Book)
            {

                Book book = (Book)informationResource;
                DisplayCardBookVM displayCardBookVM = AutoMapperConfig.Mapper.Map<DisplayCardBookVM>(book);
                return PartialView("Cards/_CardBook", displayCardBookVM);
            }
            else if (informationResource is Paper)
            {
                Paper paper = (Paper)informationResource;
                DisplayCardPaperVM displayCardPaperVM = AutoMapperConfig.Mapper.Map<DisplayCardPaperVM>(paper);
                displayCardPaperVM.PaperIssues = new EPAM.Library.MVCPL.Models.PaperIssueLogic().GetPaperIssues(paper.ISSN);
                return PartialView("Cards/_CardPaper", displayCardPaperVM);
            }
            else if (informationResource is Patent)
            {
                Patent patent = (Patent)informationResource;
                DisplayCardPatentVM displayCardPatentVM = AutoMapperConfig.Mapper.Map<DisplayCardPatentVM>(patent);
                return PartialView("Cards/_CardPatent", displayCardPatentVM);
            }
            return View();
        }

        [AuthorizeLogger(Roles = "admin, librarian")]
        public ActionResult Delete(Guid id)
        {
            DependenciesResolverConfig.DependenciesResolver.bookLogic.DeleteBook(id);
            return RedirectToAction(nameof(Index), "Main");
        }

        [AuthorizeLogger(Roles = "admin, librarian")]
        public ActionResult Edit(Guid id)
        {

            return RedirectToAction(nameof(CreateEdit), "Main", new {id = id });
        }


        public ActionResult CreateEdit(Guid id)
        {
            InformationResource informationResource = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(IR => IR.Id == id);
            if (informationResource is Book)
            {

                Book book = (Book)informationResource;
                CreateBookVM createBookVM = AutoMapperConfig.Mapper.Map<CreateBookVM>(book);
                
                return RedirectToAction("Edit", "Books", createBookVM);
                //return PartialView("Edit/_EditBook", createBookVM);
            }
            if (informationResource is Paper)
            {
                Paper paper = (Paper)informationResource;
                EditPaperVM editPaperVM = AutoMapperConfig.Mapper.Map<EditPaperVM>(paper);
                return RedirectToAction("Edit", "Papers", editPaperVM);
            }
            else if (informationResource is Patent)
            {
                AuthorsModel authorsModel = new AuthorsModel();
                Patent patent = (Patent)informationResource;
                CreatePatentVM createPatentVM = AutoMapperConfig.Mapper.Map<CreatePatentVM>(patent);
                //DependenciesResolverConfig.DependenciesResolver.patentsLogicWithRoles.AddPatent(patent);

                return RedirectToAction("Edit", "Patents", createPatentVM);
                //return View("Edit/_EditPatent", createPatentVM);
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult CreateEdit(Object model)
        //{

        //    //if (model is EditPaperVM)
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {
        //    //        EditPaperVM editPaperVM = (EditPaperVM)model;
        //    //        return RedirectToAction("Edit", "Papers");
        //    //    }
        //    //    return View((EditPaperVM)model_;
        //    //}
            

        //    //return View(model);
        //}
    }
}