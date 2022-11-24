using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.PaperIssues
{
    public class CatalogPaperIssueModel
    {
        public PaperIssue PaperIssue { get; set; }
        public Paper Paper { get; set; }
    }
}