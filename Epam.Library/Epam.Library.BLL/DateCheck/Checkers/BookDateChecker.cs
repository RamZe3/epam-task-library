using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class BookDateChecker : GeneralDateChecker
    {
        private const int MinYearOfPublishing = 1400;
        public bool IsAuthorsCorrect(List<Author> authors)
        {
            if (authors.Count == 0)
            {
                return false;
            }

            bool Iscorrect = true;
            foreach (var author in authors)
            {
                Iscorrect = Iscorrect && IsAuthorCorrect(author);
            }
            return Iscorrect;
        }

        public bool IsYearOfPublishingCorrect(int yearOfPublishing)
        {
            return (yearOfPublishing <= DateTime.Now.Year) && (yearOfPublishing >= MinYearOfPublishing);
        }

        public bool IsISBNCorrect(string ISBN)
        {
            Regex regex = new Regex(@"(^ISBN ([0-7]|(8[0-9]|9[0-4])|(9[5-8][0-9])|(99[0-3])|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X)$)");
            return String.IsNullOrEmpty(ISBN) || regex.IsMatch(ISBN);
        }
    }
}
