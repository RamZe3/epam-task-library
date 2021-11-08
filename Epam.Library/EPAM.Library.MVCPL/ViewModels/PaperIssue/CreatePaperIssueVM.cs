using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.ViewModels.PaperIssue
{
    public class CreatePaperIssueVM
    {
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
        public int NumberOfPages { get; set; }
        public List<SelectListItem> PapersList { get; set; }
    }
}