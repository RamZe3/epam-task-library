using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{

    public class Book : InformationResource, IHaveYearOfPublishing, IHaveAuthors
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public string PlaceOfPublication { get; set; }
        public string Publisher { get; set; }
        public int YearOfPublishing { get; set; }
        public string ISBN { get; set; }

        public Book(string name, Guid id, List<Author> authors, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, string iSBN) : base(id, name)
        {
            this.Authors = authors;
            this.PlaceOfPublication = placeOfPublication;
            this.Publisher = publisher;
            this.YearOfPublishing = yearOfPublishing;
            this.NumberOfPages = numberOfPages;
            this.Note = note;
            ISBN = iSBN;
        }

        public Book(string name, List<Author> authors, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, string iSBN) : base(name)
        {
            Name = name;
            Authors = authors;
            PlaceOfPublication = placeOfPublication;
            Publisher = publisher;
            YearOfPublishing = yearOfPublishing;
            NumberOfPages = numberOfPages;
            Note = note;
            ISBN = iSBN;
        }

        public int GetYearOfPublishing()
        {
            return YearOfPublishing;
        }

        public List<Author> GetAuthors()
        {
            return Authors;
        }
    }
}
