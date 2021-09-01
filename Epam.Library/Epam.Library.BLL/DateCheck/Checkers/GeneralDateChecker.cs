using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class GeneralDateChecker
    {
        private const int MaxPlaceOfPublicationSize = 300;
        private const int MaxPublisherSize = 300;
        private const int MaxNoteSize = 2000;
        private const int MaxNameSize = 300;
        private const int MaxAuthorNameSize = 50;
        private const int MaxAuthorSurnameSize = 200;
        public bool IsNameCorrect(string name)
        {
            return !String.IsNullOrEmpty(name) && name.Count() <= MaxNameSize;
        }

        public bool IsAuthorCorrect(Author author)
        {
            Regex nameRegex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ]))+$)");
            Regex surNameRegex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z])|([a-z] ([a-z]|[A-Z])))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|([а-яё] ([а-яё]|[А-ЯЁ])))+$)");

            return nameRegex.IsMatch(author.Name) & (!String.IsNullOrEmpty(author.Name) && author.Name.Count() <= MaxAuthorNameSize) &
                surNameRegex.IsMatch(author.Surname) & (!String.IsNullOrEmpty(author.Surname) && author.Surname.Count() <= MaxAuthorSurnameSize);
        }

        public bool IsNoteCorrect(string note)
        {
            return (note != null && note.Count() <= MaxNoteSize);
        }

        public bool IsPublisherCorrect(string publisher)
        {
            return !String.IsNullOrEmpty(publisher) && publisher.Count() <= MaxPublisherSize;
        }

        public bool IsPlaceOfPublicationCorrect(string placeOfPublication)
        {
            Regex regex = new Regex(@"(^[A-Z]([a-z]|(\-[A-Z])|( [A-Za-z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|( [А-ЯЁа-яё]))+$)");
            return regex.IsMatch(placeOfPublication) && (!String.IsNullOrEmpty(placeOfPublication) && placeOfPublication.Count() <= MaxPlaceOfPublicationSize);
        }

        public bool IsNumberOfPagesCorrect(int numberOfPages)
        {
            return numberOfPages >= 0;
        }
    }
}
