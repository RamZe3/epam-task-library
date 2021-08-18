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
        public bool IsInventorsCorrect(List<Author> inventors)
        {
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
            Regex countRegex = new Regex(@"^[\w\W]{1,200}$");
            return regex.IsMatch(country) && countRegex.IsMatch(country);
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
            return (dateOfApplication.Year >= 1474) && (dateOfApplication <= DateTime.Now);
        }

        public bool IsDateOfPublicationCorrect(DateTime dateOfApplication , DateTime dateOfPublication)
        {
            return (dateOfPublication < DateTime.Now) && (dateOfPublication >= dateOfApplication) && (dateOfPublication.Year >= 1474);
        }

    }
}
