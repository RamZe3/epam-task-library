using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class PatentDateChecker : GeneralDateChecker
    {
        private const int MaxCountrySize = 200;
        private const int MinYearOfApplication = 1474;

        public bool IsInventorsCorrect(List<Author> inventors)
        {
            if (inventors.Count ==0)
            {
                return false;
            }

            bool Iscorrect = true;
            foreach(var inventor in inventors)
            {
                Iscorrect = Iscorrect && IsAuthorCorrect(inventor);
            }
            return Iscorrect;
        }

        public bool IsCountryCorrect(string country)
        {
            Regex regex = new Regex(@"(^[A-Z][a-z]+$)|(^[A-Z]+$)|(^[А-ЯЁ][а-яё]+$)|(^[А-ЯЁ]+$)");
            return (!String.IsNullOrEmpty(country) && country.Count() <= MaxCountrySize) && regex.IsMatch(country);
        }

        public bool IsRegistrationNumberCorrect(int registrationNumber)
        {
            if (registrationNumber<=999999999 && registrationNumber >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDateOfApplicationCorrect(DateTime dateOfApplication)
        {
            return (dateOfApplication.Year >= MinYearOfApplication) && (dateOfApplication <= DateTime.Now);
        }

        public bool IsDateOfPublicationCorrect(DateTime dateOfApplication , DateTime dateOfPublication)
        {
            return (dateOfPublication < DateTime.Now) && (dateOfPublication >= dateOfApplication) && (dateOfPublication.Year >= MinYearOfApplication);
        }

    }
}
