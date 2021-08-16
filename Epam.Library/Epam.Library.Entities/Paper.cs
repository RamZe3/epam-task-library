using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class Paper : InformationResource, IHaveYearOfPublishing
    {
        public string placeOfPublication { get; set; }
        public string publisher { get; set; }
        public int yearOfPublishing { get; set; }
        public int numberOfPages { get; set; }
        public string note { get; set; }
        public int number { get; set; }
        public DateTime date { get; set; }
        public string ISSN { get; set; }

        public Paper(string name, Guid id, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, int number, DateTime date, string iSSN) : base(id, name)
        {
            this.placeOfPublication = placeOfPublication;
            this.publisher = publisher;
            this.yearOfPublishing = yearOfPublishing;
            this.numberOfPages = numberOfPages;
            this.note = note;
            this.number = number;
            this.date = date;
            ISSN = iSSN;
        }



        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public int GetYearOfPublishing()
        {
            return yearOfPublishing;
        }
    }
}
