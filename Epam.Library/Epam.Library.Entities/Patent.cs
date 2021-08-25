using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class Patent : InformationResource, IHaveYearOfPublishing, IHaveAuthors
    {
        public List<Author> Inventors { get; set; } = new List<Author>();
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfPublication { get; set; }

        public Patent(string name, Guid id, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note) : base(id, name)
        {
            this.Inventors = inventors;
            this.Country = country;
            this.RegistrationNumber = registrationNumber;
            this.DateOfApplication = dateOfApplication;
            this.DateOfPublication = dateOfPublication;
            this.NumberOfPages = numberOfPages;
            this.Note = note;
        }

        public Patent(string name, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note) : base(name)
        {
            Inventors = inventors;
            Country = country;
            RegistrationNumber = registrationNumber;
            DateOfApplication = dateOfApplication;
            DateOfPublication = dateOfPublication;
            NumberOfPages = numberOfPages;
            Note = note;
        }

        public int GetYearOfPublishing()
        {
            return DateOfPublication.Year;
        }

        public List<Author> GetAuthors()
        {
            return Inventors;
        }

        public int GetYearOfPublication()
        {
            return DateOfPublication.Year;
        }
    }
}
