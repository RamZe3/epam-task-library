using Epam.Library.Entities;
using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.Resource
{
    public class DisplayResourceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }

        [Display(Name= "Number of pages")]
        public int NumberOfPages { get; set; }
        public List<InformationResource> Resources { get; set; }
        public PageInfo PageInfo { get; set; }

        [Required]
        public string NametoSearch { get; set; }
    }
}