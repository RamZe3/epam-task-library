using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.DAL.Interfaces
{
    public interface IInformationResourceDAL
    {
        List<InformationResource> GetLibrary();
        InformationResource FindResourceByName(string name);
        List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse);
        List<Book> FindBooksByAuthor(Author author);
        List<Patent> FindPatentsByAuthor(Author author);
        List<InformationResource> FindPatentsAndBooksByAuthor(Author author);
        List<Book> SmartBookSearchByPublisher(string str);
        List<InformationResource> GroupingResourceByYearOfPublication();
    }
}