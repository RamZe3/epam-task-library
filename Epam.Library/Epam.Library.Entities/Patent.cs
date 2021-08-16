using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class Patent : InformationResource, IHaveYearOfPublishing, IHaveAuthors, IHaveYearOfPublication
    {
        public List<Author> inventors { get; set; }
        public string country { get; set; }
        public int registrationNumber { get; set; }
        public DateTime dateOfApplication { get; set; }
        public DateTime dateOfPublication { get; set; }
        public int numberOfPages { get; set; }
        public string note { get; set; }

        public Patent(string name, Guid id, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note) : base(id, name)
        {
            this.inventors = inventors;
            this.country = country;
            this.registrationNumber = registrationNumber;
            this.dateOfApplication = dateOfApplication;
            this.dateOfPublication = dateOfPublication;
            this.numberOfPages = numberOfPages;
            this.note = note;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public int GetYearOfPublishing()
        {
            return dateOfPublication.Year;
        }

        public List<Author> GetAuthors()
        {
            return inventors;
        }

        public int GetYearOfPublication()
        {
            return dateOfPublication.Year;
        }
    }
}
