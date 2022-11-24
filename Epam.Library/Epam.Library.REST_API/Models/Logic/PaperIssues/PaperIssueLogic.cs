using Epam.Library.Entities;
using Epam.Library.REST_API.Models.PaperIssues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Logic.PaperIssues
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

        public List<PaperIssue> GetPaperIssues(string ISSN)
        {
            IEnumerable<Paper> papers = DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().OfType<Paper>();
            IEnumerable<Paper> papersWithISSN = papers.Where(x => x.ISSN == ISSN);
            IEnumerable<PaperIssue> paperIssues = papersWithISSN.Select(paper => AdaptPaperToPaperIsuue(paper));
            return paperIssues.ToList();
        }

        public PaperIssue GetPaperIssueById(Guid id)
        {
            Paper paper = (Paper)DependenciesResolverConfig.DependenciesResolver.InformationResourceLogic.GetLibrary().Find(x => x.Id == id);
            PaperIssue paperIssue = AdaptPaperToPaperIsuue(paper);
            return paperIssue;
        }

    }
}