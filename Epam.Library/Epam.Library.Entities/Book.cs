using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{

    public class Book : InformationResource
    {
        public List<Author> authors { get; set; }
        public string placeOfPublication { get; set; }
        public string publisher { get; set; }
        public int yearOfPublishing { get; set; }
        public int numberOfPages { get; set; }
        public string note { get; set; }
        public string ISBN { get; set; }

        public Book(string name, Guid id, List<Author> authors, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, string iSBN) : base(id, name)
        {
            this.authors = authors;
            this.placeOfPublication = placeOfPublication;
            this.publisher = publisher;
            this.yearOfPublishing = yearOfPublishing;
            this.numberOfPages = numberOfPages;
            this.note = note;
            ISBN = iSBN;
        }

        public Book(string name, Guid id, string iSBN) : base(id, name)
        {
            ISBN = iSBN;
        }

        public override string ToString()
        {
            return name + " " + ISBN;
        }
    }
}
