using Epam.Library.Entities;
using Epam.Library.REST_API.Models.PaperIssues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Papers
{
    public class CatalogPaperModel
    {
        public Paper Paper { get; set; }
        public List<PaperIssue> PaperIssues { get; set; }
    }
}