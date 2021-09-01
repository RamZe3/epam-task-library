using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class ComparisonerResources
    {
        public bool CompareAuthors(List<Author> authors1, List<Author> authors2)
        {
            foreach (var author in authors1)
            {
                if (!authors2.Contains(author))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CompareBooks(Book book1, Book book2)
        {
            
            if (!String.IsNullOrEmpty(book1.ISBN) && !String.IsNullOrEmpty(book2.ISBN))
            {
                return book1.ISBN == book2.ISBN;
            }
            else
            {
                return book1.Name == book2.Name &&
                    book1.Publisher == book2.Publisher &&
                    CompareAuthors(book1.Authors, book2.Authors);
            }
        }

        public bool ComparePaper(Paper paper1, Paper paper2)
        {
            if (!String.IsNullOrEmpty(paper1.ISSN) && !String.IsNullOrEmpty(paper2.ISSN))
            {
                return paper1.ISSN == paper2.ISSN &&
                    paper1.Name != paper2.Name;
            }
            else
            {
                return paper1.Name == paper2.Name &&
                    paper1.Publisher == paper2.Publisher &&
                    paper1.Date == paper2.Date;
            }
        }

        public bool ComparePatent(Patent patent1, Patent patent2)
        {
            return patent1.RegistrationNumber == patent2.RegistrationNumber &&
                patent1.Country == patent2.Country;
        }
    }
}
