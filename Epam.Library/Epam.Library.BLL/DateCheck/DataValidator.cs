using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
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

        #region IsBookCorrect
        public List<DataValidationError> IsBookCorrect(Book book)
        {
            List<DataValidationError> dataValidationExceptions = new List<DataValidationError>();

            if (!_BookDateChecker.IsNameCorrect(book.Name) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Name validation exception", book.Name));
            }
            if (!_BookDateChecker.IsAuthorsCorrect(book.Authors) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Authors validation exception", ""));
            }
            if (!_BookDateChecker.IsPlaceOfPublicationCorrect(book.PlaceOfPublication) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("PlaceOfPublication validation exception", book.PlaceOfPublication));
            }
            if (!_BookDateChecker.IsPublisherCorrect(book.Publisher) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Publisher validation exception", book.Publisher));
            }
            if (!_BookDateChecker.IsYearOfPublishingCorrect(book.YearOfPublishing) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("YearOfPublishing validation exception", book.YearOfPublishing.ToString()));
            }
            if (!_BookDateChecker.IsNumberOfPagesCorrect(book.NumberOfPages) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("NumberOfPages validation exception", book.NumberOfPages.ToString()));
            }
            if (!_BookDateChecker.IsNoteCorrect(book.Note) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Note validation exception", book.Note));
            }
            if (!_BookDateChecker.IsISBNCorrect(book.ISBN) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("ISBN validation exception", book.ISBN));
            }

            return dataValidationExceptions;
        }
        #endregion

        #region IsPaperCorrect
        public List<DataValidationError> IsPaperCorrect(Paper paper)
        {
            List<DataValidationError> dataValidationExceptions = new List<DataValidationError>();

            if (!_PaperDateChecker.IsNameCorrect(paper.Name) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Name validation exception", paper.Name));
            }
            if (!_PaperDateChecker.IsPublisherCorrect(paper.Publisher) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Publisher validation exception", paper.Publisher));
            }
            if (!_PaperDateChecker.IsPlaceOfPublicationCorrect(paper.PlaceOfPublication) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("PlaceOfPublication validation exception", paper.PlaceOfPublication));
            }
            if (!_PaperDateChecker.IsYearOfPublishingCorrect(paper.YearOfPublishing) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("YearOfPublishing validation exception", paper.YearOfPublishing.ToString()));
            }
            if (!_PaperDateChecker.IsNumberOfPagesCorrect(paper.NumberOfPages) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("NumberOfPages validation exception", paper.NumberOfPages.ToString()));
            }
            if (!_PaperDateChecker.IsNoteCorrect(paper.Note) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Note validation exception", paper.Note));
            }
            if (!_PaperDateChecker.IsNumberCorrect(paper.Number) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Number validation exception", paper.Number.ToString()));
            }
            if (!_PaperDateChecker.IsDateCorrect(paper.YearOfPublishing, paper.Date) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Date validation exception", paper.Date.ToString()));
            }
            if (!_PaperDateChecker.IsISSNCorrect(paper.ISSN) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("ISSN validation exception", paper.ISSN));
            }


            return dataValidationExceptions;
        }
        #endregion

        #region IsPatentCorrect
        public List<DataValidationError> IsPatentCorrect(Patent patent)
        {
            List<DataValidationError> dataValidationExceptions = new List<DataValidationError>();

            if (!_PatentDateChecker.IsNameCorrect(patent.Name) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Name validation exception", patent.Name));
            }
            if (!_PatentDateChecker.IsInventorsCorrect(patent.Inventors) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Inventors validation exception", ""));
            }
            if (!_PatentDateChecker.IsCountryCorrect(patent.Country) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Country validation exception", patent.Country));
            }
            if (!_PatentDateChecker.IsRegistrationNumberCorrect(patent.RegistrationNumber) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("RegistrationNumber validation exception", patent.RegistrationNumber.ToString()));
            }
            if (!_PatentDateChecker.IsDateOfApplicationCorrect(patent.DateOfApplication) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("DateOfApplication validation exception", patent.DateOfApplication.ToString()));
            }
            if (!_PatentDateChecker.IsDateOfPublicationCorrect(patent.DateOfApplication, patent.DateOfPublication) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("DateOfPublication validation exception", patent.DateOfPublication.ToString()));
            }
            if (!_PatentDateChecker.IsNumberOfPagesCorrect(patent.NumberOfPages) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("NumberOfPages validation exception", patent.NumberOfPages.ToString()));
            }
            if (!_PatentDateChecker.IsNoteCorrect(patent.Note) == true)
            {
                dataValidationExceptions
                    .Add(new DataValidationError("Note validation exception", patent.Note));
            }

            return dataValidationExceptions;
        }
        #endregion
    }
}
