using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Interfaces;
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
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            Paper newPaper = new Paper(name, new Guid(), placeOfPublication, publisher, yearOfPublishing, numberOfPages, note, number, date, iSSN);
            if (!_dataValidator.IsPaperCorrect(newPaper))
            {
                throw new Exception("Vadidator Eror");
            }

            foreach (var resource in Library)
            {
                if (resource is Paper)
                {
                    Paper paper = (Paper)resource;
                    if (_comparisonerResources.ComparePaper(newPaper, paper))
                    {
                        throw new Exception("Compare Ex");
                    }
                }
            }

            _informationResourceDAL.AddPaper(newPaper);
        }

        public void AddPatent(string name, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            Patent newPatent = new Patent(name, new Guid(), inventors, country, registrationNumber, dateOfApplication, dateOfPublication, numberOfPages, note);
            if (!_dataValidator.IsPatentCorrect(newPatent))
            {
                throw new Exception("Vadidator Eror");
            }

            foreach (var resource in Library)
            {
                if (resource is Patent)
                {
                    Patent patent = (Patent)resource;
                    if (_comparisonerResources.ComparePatent(newPatent, patent))
                    {
                        throw new Exception("Compare Ex");
                    }
                }
            }

            _informationResourceDAL.AddPatent(newPatent);
        }

        public void DeleteResource(Guid guid)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            foreach (var resource in Library)
            {
                if (resource.id == guid)
                {
                    _informationResourceDAL.DeleteResource(guid);
                    return;
                }
            }

            throw new Exception("Delete Error");
        }

        public List<Book> FindBooksByAuthor(Author author)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable <Book> books =
                from resource in Library
                where resource is Book
                select (Book)resource;

            books = books.ToList();

            List<Book> answerBooks = new List<Book>();
            foreach (var book in books)
            {
                foreach (var authorOfBook in book.authors)
                {
                    if (authorOfBook == author)
                    {
                        answerBooks.Add(book);
                    }
                }
            }

            return answerBooks;
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable<IHaveAuthors> haveAuthors =
                from resource in Library
                where (resource is Book) || (resource is Patent)
                select (IHaveAuthors)resource;

            List<InformationResource> resources = new List<InformationResource>();
            foreach (var resource in haveAuthors)
            {
                foreach (var authorOfBook in resource.GetAuthors())
                {
                    if (authorOfBook == author)
                    {
                        resources.Add((InformationResource)resource);
                    }
                }
            }

            return resources;
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable<Patent> patents =
                from resource in Library
                where resource is Patent
                select (Patent)resource;

            patents = patents.ToList();

            List<Patent> answerPatents = new List<Patent>();
            foreach (var patent in patents)
            {
                foreach (var authorOfPattent in patent.inventors)
                {
                    if (authorOfPattent == author)
                    {
                        answerPatents.Add(patent);
                    }
                }
            }

            return answerPatents;
        }

        public InformationResource FindResourceByName(string name)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            InformationResource resource = Library.Single(res => res.name == name);
            return resource;
        }

        public List<InformationResource> GetLibrary()
        {
            return _informationResourceDAL.GetLibrary();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing()
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable<IHaveYearOfPublishing> IHaveYearOfPublishingresources =
                from resource in Library
                where (resource is IHaveYearOfPublishing)
                select (IHaveYearOfPublishing)resource;

            IEnumerable<InformationResource> resources =
                from resource in IHaveYearOfPublishingresources
                orderby resource.GetYearOfPublishing() descending
                select (InformationResource)resource;

            return resources.ToList();
        }

        public List<InformationResource> GroupingResourceByYearOfPublication()
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable<InformationResource> resources =
                (IEnumerable<InformationResource>)(from resource in Library
                group resource by (resource is IHaveYearOfPublication) into newGroup
                select newGroup);

            return resources.ToList();
        }

        public List<Book> SmartBookSearchByPublisher(string str)
        {
            List<InformationResource> Library = _informationResourceDAL.GetLibrary();
            IEnumerable<Book> books =
                from resource in Library
                where resource is Book
                select (Book)resource;

            books = books.ToList();

            List<Book> answerBooks = new List<Book>();
            foreach (var book in books)
            {
                if (book.publisher.Contains(str))
                {
                    answerBooks.Add(book);
                }
            }

            return answerBooks;
        }
    }
}
