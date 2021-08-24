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
        List<InformationResource> FindResourcesByName(string name);
        List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse);
        List<Book> FindBooksByAuthor(Author author);
        List<Patent> FindPatentsByAuthor(Author author);
        List<InformationResource> FindPatentsAndBooksByAuthor(Author author);
        Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str);
        Dictionary<int, List<InformationResource>> GroupingResourceByYearOfPublication();
    }
}