using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Epam.Library.Entities;

namespace EPAM.Library.MVCPL.ViewModels.Patent
{
    public class DisplayCardPatentVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Note { get; set; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfPublication { get; set; }

        public List<Epam.Library.Entities.Author> Inventors  { get; set; }

    }
}