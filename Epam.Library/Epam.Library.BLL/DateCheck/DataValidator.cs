using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public class DataValidator : IDataValidator
    {
        private static BookDateChecker _BookDateChecker = new BookDateChecker();
        private static PaperDateChecker _PaperDateChecker = new PaperDateChecker();
        private static PatentDateChecker _PatentDateChecker = new PatentDateChecker();

        public bool IsBookCorrect(Book book)
        {
            return _BookDateChecker.IsNameCorrect(book.Name) &&
                _BookDateChecker.IsAuthorsCorrect(book.Authors) &&
                _BookDateChecker.IsPlaceOfPublicationCorrect(book.PlaceOfPublication) &&
                _BookDateChecker.IsPublisherCorrect(book.Publisher) &&
                _BookDateChecker.IsYearOfPublishingCorrect(book.YearOfPublishing) &&
                _BookDateChecker.IsNumberOfPagesCorrect(book.NumberOfPages) &&
                _BookDateChecker.IsNoteCorrect(book.Note) &&
                _BookDateChecker.IsISBNCorrect(book.ISBN);
        }

        public bool IsPaperCorrect(Paper paper)
        {
            return _PaperDateChecker.IsNameCorrect(paper.Name) &&
                _PaperDateChecker.IsPlaceOfPublicationCorrect(paper.PlaceOfPublication) &&
                _PaperDateChecker.IsPublisherCorrect(paper.Publisher) &&
                _PaperDateChecker.IsYearOfPublishingCorrect(paper.YearOfPublishing) &&
                _PaperDateChecker.IsNumberOfPagesCorrect(paper.NumberOfPages) &&
                _PaperDateChecker.IsNoteCorrect(paper.Note) &&
                _PaperDateChecker.IsNumberCorrect(paper.Number) &&
                _PaperDateChecker.IsDateCorrect(paper.YearOfPublishing, paper.Date) &&
                _PaperDateChecker.IsISSNCorrect(paper.ISSN);
        }

        public bool IsPatentCorrect(Patent patent)
        {
            return _PatentDateChecker.IsNameCorrect(patent.Name) &&
                _PatentDateChecker.IsInventorsCorrect(patent.Inventors) &&
                _PatentDateChecker.IsCountryCorrect(patent.Country) &&
                _PatentDateChecker.IsRegistrationNumberCorrect(patent.RegistrationNumber) &&
                _PatentDateChecker.IsDateOfApplicationCorrect(patent.DateOfApplication) &&
                _PatentDateChecker.IsDateOfPublicationCorrect(patent.DateOfApplication, patent.DateOfPublication) &&
                _PatentDateChecker.IsNumberOfPagesCorrect(patent.NumberOfPages) &&
                _PatentDateChecker.IsNoteCorrect(patent.Note);
        }
    }
}
