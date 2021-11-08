using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.PaperIssue
{
    public class DisplayPaperIssueVM
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }
}