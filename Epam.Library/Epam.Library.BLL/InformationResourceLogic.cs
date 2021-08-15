using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL
{
    public class InformationResourceLogic : IInformationResourceLogic
    {
        private IInformationResourceDAL _informationResourceDAL;
        private DataValidator _dataValidator;
        private ComparisonerResources _comparisonerResources;

        public InformationResourceLogic(IInformationResourceDAL informationResourceDAL)
        {
            _dataValidator = new DataValidator();
            _comparisonerResources = new ComparisonerResources();
            _informationResourceDAL = informationResourceDAL;
        }

        public void AddBook(string name, List<Author> authors, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, string iSBN)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            Book newBook = new Book(name, new Guid(), authors, placeOfPublication, publisher, yearOfPublishing, numberOfPages, note, iSBN);

            if (!_dataValidator.IsBookCorrect(newBook))
            {
                throw new Exception("Vadidator Eror");
            }

            foreach (var resource in Library)
            {
                if (resource is Book)
                {
                    Book book = (Book)resource;
                    if (_comparisonerResources.CompareBooks(newBook, book))
                    {
                        throw new Exception("Compare Ex");
                    }
                }
            }

            _informationResourceDAL.AddBook(newBook);
        }

        public void AddPaper(string name, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, int number, DateTime date, string iSSN)
        {
            throw new NotImplementedException();
        }

        public void AddPatent(string name, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note)
        {
            throw new NotImplementedException();
        }

        public void DeleteResource(Guid guid)
        {
            throw new NotImplementedException();
        }

        public List<Book> FindBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public InformationResource FindResourceByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetLibrary()
        {
            return _informationResourceDAL.GetLibrary();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing()
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GroupingResourceByYearOfPublication()
        {
            throw new NotImplementedException();
        }

        public List<Book> SmartBookSearchByPublisher(string str)
        {
            throw new NotImplementedException();
        }
    }
}
