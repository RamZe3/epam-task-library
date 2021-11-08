using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Models
{
    public class PaperIssueLogic
    {
        public Paper AdaptPaperIsuueToPaper(PaperIssue paperIssue)
        {
            Paper paper = (Paper)DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(x => x.Id == paperIssue.Id);
            paper.Id = paperIssue.Id;
            paper.Number = paperIssue.Number;
            paper.NumberOfPages = paperIssue.NumberOfPages;
            paper.ISSN = paperIssue.ISSN;
            paper.Date = paperIssue.Date;
            return paper;
        }

        public PaperIssue AdaptPaperToPaperIsuue(Paper paper)
        {
            PaperIssue paperIssue = new PaperIssue();
            paperIssue.Id = paper.Id;
            paperIssue.Number = paper.Number;
            paperIssue.NumberOfPages = paper.NumberOfPages;
            paperIssue.ISSN = paper.ISSN;
            paperIssue.Date = paper.Date;
            return paperIssue;
        }

        //public List<Paper> GetPapers()
        //{
        //    IEnumerable<Paper> papers = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().OfType<Paper>();
        //    papers.Distinct();
        //    papers.Distinct()
        //}

        public List<PaperIssue> GetPaperIssues(string ISSN)
        {
            IEnumerable<Paper> papers = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().OfType<Paper>();
            IEnumerable<Paper> papersWithISSN = papers.Where(x => x.ISSN == ISSN);
            IEnumerable<PaperIssue> paperIssues = papersWithISSN.Select(paper => AdaptPaperToPaperIsuue(paper));
            return paperIssues.ToList();
        }

        public List<SelectListItem> GetPapersSelectListItems()
        {
            IEnumerable<Paper> papers = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().OfType<Paper>();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var paper in papers)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = paper.Name,
                    Value = paper.ISSN
                };
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        public List<SelectListItem> GetSelectListItems()
        {
            List<Author> authors = DependenciesResolverConfig.DependenciesResolver.authorSQLDAL.GetAuthors();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var author in authors)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = author.Name + " " + author.Surname,
                    Value = author.Id.ToString()
                };
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        public PaperIssue GetPaperIssueById(Guid id)
        {
            Paper paper = (Paper)DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(x => x.Id == id);
            PaperIssue paperIssue = AdaptPaperToPaperIsuue(paper);
            return paperIssue;
        }

    }
}