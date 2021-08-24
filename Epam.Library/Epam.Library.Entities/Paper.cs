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
        public string PlaceOfPublication { get; set; }
        public string Publisher { get; set; }
        public int YearOfPublishing { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }

        public Paper(string name, Guid id, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, int number, DateTime date, string iSSN) : base(id, name)
        {
            this.PlaceOfPublication = placeOfPublication;
            this.Publisher = publisher;
            this.YearOfPublishing = yearOfPublishing;
            this.NumberOfPages = numberOfPages;
            this.Note = note;
            this.Number = number;
            this.Date = date;
            ISSN = iSSN;
        }

        public Paper(string name, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, int number, DateTime date, string iSSN) : base(name)
        {
            PlaceOfPublication = placeOfPublication;
            Publisher = publisher;
            YearOfPublishing = yearOfPublishing;
            Number = number;
            NumberOfPages = numberOfPages;
            Note = note;
            Date = date;
            ISSN = iSSN;
        }

        public int GetYearOfPublishing()
        {
            return YearOfPublishing;
        }
    }
}
