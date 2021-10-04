using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class InformationResourceSQLDAL : IInformationResourceDAL
    {
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

        public List<InformationResource> FindResourcesByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetLibrary()
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<InformationResource>> GroupingResourceByYearOfPublication()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str)
        {
            throw new NotImplementedException();
        }
    }
}
