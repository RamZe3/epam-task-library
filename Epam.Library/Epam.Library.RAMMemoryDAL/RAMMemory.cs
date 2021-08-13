using Epam.Library.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class RAMMemory : IInformationResourceDAL
    {
        public void AddBook(Library.Entities.Book book)
        {
            throw new NotImplementedException();
        }

        public void AddPaper(Library.Entities.Paper paper)
        {
            throw new NotImplementedException();
        }

        public void AddPatent(Library.Entities.Patent patent)
        {
            throw new NotImplementedException();
        }

        public void DeleteResource(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Library.Entities.InformationResource> GetLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
