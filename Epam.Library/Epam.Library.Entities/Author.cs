using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class Author
    {
        public string name { get; set; }
        public string surname { get; set; }

        public Author(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public static bool operator ==(Author author1, Author author2)
        {
            return author1.name == author2.name && author1.surname == author2.surname;
        }

        public static bool operator !=(Author author1, Author author2)
        {
            return author1.name != author2.name || author1.surname != author2.surname;
        }
    }
}
