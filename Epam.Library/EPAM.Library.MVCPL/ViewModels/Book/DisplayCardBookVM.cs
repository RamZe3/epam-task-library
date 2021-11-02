using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.Book
{
    public class DisplayCardBookVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Note { get; set; }
        public string PlaceOfPublication { get; set; }
        public string Publisher { get; set; }
        public int YearOfPublishing { get; set; }
        public string ISBN { get; set; }
        public List<Epam.Library.Entities.Author> Authors { get; set; }
    }
}