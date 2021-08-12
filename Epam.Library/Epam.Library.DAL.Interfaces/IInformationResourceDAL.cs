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
        void AddBook(Book book);
        void AddPaper(Paper paper);
        void AddPatent(Patent patent);
        void DeleteResource(Guid id);
        List<InformationResource> GetLibrary();
    }
}
