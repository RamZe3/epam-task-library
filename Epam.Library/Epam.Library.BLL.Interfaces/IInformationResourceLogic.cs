using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces
{
    public interface IInformationResourceLogic
    {
        void AddBook(string name, List<Author> authors, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, string iSBN);
        void AddPaper(string name, string placeOfPublication, string publisher, int yearOfPublishing, int numberOfPages, string note, int number, DateTime date, string iSSN);
        void AddPatent(string name, List<Author> inventors, string country, int registrationNumber, DateTime dateOfApplication, DateTime dateOfPublication, int numberOfPages, string note);
        void DeleteResource(Guid guid);
        List<InformationResource> GetLibrary();
        InformationResource FindResourceByName(string name);
        List<InformationResource> GetSortedLibraryByYearOfPublishing();
        List<Book> FindBooksByAuthor(Author author);
        List<Patent> FindPatentsByAuthor(Author author);
        List<InformationResource> FindPatentsAndBooksByAuthor(Author author);
        List<Book> SmartBookSearchByPublisher(string str);
        List<InformationResource> GroupingResourceByYearOfPublication();
    }
}
