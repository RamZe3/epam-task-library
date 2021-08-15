using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class RAMMemory : IInformationResourceDAL
    {
        public static List<InformationResource> Library = new List<InformationResource>();
        public void AddBook(Book book)
        {
            Library.Add(book);
        }

        public void AddPaper(Paper paper)
        {
            Library.Add(paper);
        }

        public void AddPatent(Patent patent)
        {
            Library.Add(patent);
        }

        public void DeleteResource(Guid id)
        {
            foreach (var resource in Library)
            {
                if (resource.id == id)
                {
                    Library.Remove(resource);
                    break;
                }
            }
        }

        public List<InformationResource> GetLibrary()
        {
            return Library;
        }
    }
}
