using Epam.Library.Entities;
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
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLibrary()
        {
            DisplayResourceVM displayResourceVM = new DisplayResourceVM();

            displayResourceVM.Resources = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary();

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

        public ActionResult Delete(Guid id)
        {
            DependenciesResolverConfig.DependenciesResolver.bookLogic.DeleteBook(id);
            return RedirectToAction(nameof(Index), "Main");
        }

        public ActionResult Edit(Guid id)
        {

            return RedirectToAction(nameof(CreateEdit), "Main", new {id = id });
        }

        public ActionResult CreateEdit(Guid id)
        {
            InformationResource informationResource = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(IR => IR.Id == id);
            //if (informationResource is Book)
            //{

            //    Book book = (Book)informationResource;
            //    DisplayCardBookVM displayCardBookVM = AutoMapperConfig.Mapper.Map<DisplayCardBookVM>(book);
            //    return PartialView("Cards/_CardBook", displayCardBookVM);
            //}
            if (informationResource is Paper)
            {
                Paper paper = (Paper)informationResource;
                EditPaperVM editPaperVM = AutoMapperConfig.Mapper.Map<EditPaperVM>(paper);
                return View("Edit/EditPaper", editPaperVM);
            }
            //else if (informationResource is Patent)
            //{
            //    Patent patent = (Patent)informationResource;
            //    DisplayCardPatentVM displayCardPatentVM = AutoMapperConfig.Mapper.Map<DisplayCardPatentVM>(patent);
            //    return PartialView("Cards/_CardPatent", displayCardPatentVM);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult CreateEdit(EditPaperVM model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index), "Main");
            }

            return View(model);
        }
    }
}