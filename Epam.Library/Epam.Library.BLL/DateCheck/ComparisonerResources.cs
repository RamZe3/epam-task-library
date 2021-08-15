using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class ComparisonerResources
    {
        private static bool CompareAuthors(List<Author> authors1, List<Author> authors2)
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
            if ((book1.ISBN != "") && (book2.ISBN != ""))
            {
                return book1.ISBN == book2.ISBN;
            }
            else
            {
                return book1.name == book2.name &&
                    book1.publisher == book2.publisher &&
                    CompareAuthors(book1.authors, book2.authors);
            }
        }

        public bool ComparePaper(Paper paper1, Paper paper2)
        {
            if ((paper1.ISSN != "") && (paper2.ISSN != ""))
            {
                return paper1.ISSN == paper2.ISSN;
            }
            else
            {
                return paper1.name == paper2.name &&
                    paper1.publisher == paper2.publisher &&
                    paper1.date == paper2.date;
            }
        }

        public bool ComparePatent(Patent patent1, Patent patent2)
        {
            return patent1.registrationNumber == patent2.registrationNumber &&
                patent1.country == patent2.country;
        }
    }
}
