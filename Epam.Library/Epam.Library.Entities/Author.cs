using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public class Author
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid Id { get; set; }

        public Author(string name, string surname)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Surname = surname;
        }

        public static bool operator ==(Author author1, Author author2)
        {
            return author1.Name == author2.Name && author1.Surname == author2.Surname;
        }

        public static bool operator !=(Author author1, Author author2)
        {
            return author1.Name != author2.Name || author1.Surname != author2.Surname;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Author author = (Author)obj;
                return Name == author.Name && Surname == author.Surname;
            }
        }

        public override int GetHashCode()
        {
            string HashCode = String.Format("{0}_{1}", Name, Surname);
            return HashCode.GetHashCode();
        }
    }
}
