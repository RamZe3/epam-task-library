using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.Paper
{
    public class DisplayCardPaperVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Note { get; set; }
        public string PlaceOfPublication { get; set; }
        public string Publisher { get; set; }
        public int YearOfPublishing { get; set; }

        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }

        public List<EPAM.Library.MVCPL.Models.PaperIssue> PaperIssues { get; set; }
    }
}