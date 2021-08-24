using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class PaperDateChecker : GeneralDateChecker
    {
        private const int MinYearOfPublishing = 1400;

        public bool IsYearOfPublishingCorrect(int yearOfPublishing)
        {
            return (yearOfPublishing <= DateTime.Now.Year) && (yearOfPublishing >= MinYearOfPublishing);
        }

        public bool IsNumberCorrect(int number)
        {
            if (number >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDateCorrect(int yearOfPublishing, DateTime date)
        {
            return yearOfPublishing == date.Year;
        }

        public bool IsISSNCorrect(string ISSN)
        {
            Regex regex = new Regex(@"(^ISSN \d{4}-\d{4}$)");
            return String.IsNullOrEmpty(ISSN) || regex.IsMatch(ISSN);
        }
    }
}
