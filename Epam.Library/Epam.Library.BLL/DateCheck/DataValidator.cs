using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class DataValidator
    {
        private static BookDateChecker _BookDateChecker = new BookDateChecker();
        private static PaperDateChecker _PaperDateChecker = new PaperDateChecker();
        private static PatentDateChecker _PatentDateChecker = new PatentDateChecker();

        public bool IsBookCorrect(Book book)
        {
            return _BookDateChecker.IsNameCorrect(book.name) &&
                _BookDateChecker.IsAuthorsCorrect(book.authors) &&
                _BookDateChecker.IsPlaceOfPublicationCorrect(book.placeOfPublication) &&
                _BookDateChecker.IsPublisherCorrect(book.publisher) &&
                _BookDateChecker.IsYearOfPublishingCorrect(book.yearOfPublishing) &&
                _BookDateChecker.IsNumberOfPagesCorrect(book.numberOfPages) &&
                _BookDateChecker.IsNoteCorrect(book.note) &&
                _BookDateChecker.IsISBNCorrect(book.ISBN);
        }

        public bool IsPaperCorrect(Paper paper)
        {
            return _PaperDateChecker.IsNameCorrect(paper.name) &&
                _PaperDateChecker.IsPlaceOfPublicationCorrect(paper.placeOfPublication) &&
                _PaperDateChecker.IsPublisherCorrect(paper.publisher) &&
                _PaperDateChecker.IsYearOfPublishingCorrect(paper.yearOfPublishing) &&
                _PaperDateChecker.IsNumberOfPagesCorrect(paper.numberOfPages) &&
                _PaperDateChecker.IsNoteCorrect(paper.note) &&
                _PaperDateChecker.IsNumberCorrect(paper.number) &&
                _PaperDateChecker.IsDateCorrect(paper.yearOfPublishing, paper.date) &&
                _PaperDateChecker.IsISSNCorrect(paper.ISSN);
        }

        public bool IsPatentCorrect(Patent patent)
        {
            return _PatentDateChecker.IsNameCorrect(patent.name) &&
                _PatentDateChecker.IsInventorsCorrect(patent.inventors) &&
                _PatentDateChecker.IsCountryCorrect(patent.country) &&
                _PatentDateChecker.IsRegistrationNumberCorrect(patent.registrationNumber) &&
                _PatentDateChecker.IsDateOfApplicationCorrect(patent.dateOfApplication) &&
                _PatentDateChecker.IsDateOfPublicationCorrect(patent.dateOfApplication, patent.dateOfPublication) &&
                _PatentDateChecker.IsNumberOfPagesCorrect(patent.numberOfPages) &&
                _PatentDateChecker.IsNoteCorrect(patent.note);
        }
    }
}
