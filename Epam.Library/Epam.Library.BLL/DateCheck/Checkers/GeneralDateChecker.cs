using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Library.BLL
{
    public class GeneralDateChecker
    {
        public bool IsNameCorrect(string name)
        {
            Regex regex = new Regex(@"^[\w\W]{1,300}$");
            return regex.IsMatch(name);
        }

        protected static bool IsAuthorCorrect(Author author)
        {
            Regex nameRegex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ]))+$)");
            Regex countNameRegex = new Regex(@"^[\w\W]{1,50}$");
            Regex surNameRegex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z])|([De,Di,Fon] [A-Z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|([Де,Ди,Фон] [А-ЯЁ]))+$)");
            Regex countSurNameRegex = new Regex(@"^[\w\W]{1,200}$");

            return nameRegex.IsMatch(author.name) & countNameRegex.IsMatch(author.name) &
                surNameRegex.IsMatch(author.surname) & countSurNameRegex.IsMatch(author.surname);
        }

        public bool IsNoteCorrect(string note)
        {
            Regex regex = new Regex(@"^[\w\W]{0,2000}$");
            return regex.IsMatch(note);
        }

        public bool IsPublisherCorrect(string publisher)
        {
            Regex regex = new Regex(@"^[\w\W]{1,300}$");
            return regex.IsMatch(publisher);
        }

        public bool IsPlaceOfPublicationCorrect(string placeOfPublication)
        {
            Regex regex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z])|( [A-Za-z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|( [А-ЯЁа-яё]))+$)");
            Regex countRegex = new Regex(@"^[\w\W]{1,300}$");
            return regex.IsMatch(placeOfPublication) && countRegex.IsMatch(placeOfPublication);
        }

        public bool IsNumberOfPagesCorrect(int numberOfPages)
        {
            return numberOfPages >= 0;
        }
    }
}
