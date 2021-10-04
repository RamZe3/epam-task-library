using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class BookRAMDAL : IBookDAL
    {
        private ComparisonerResources _comparisonerResources = new ComparisonerResources();
        public bool AddBook(Book newBook)
        {
            foreach (var resource in RAMMemory.Library)
            {
                if (resource is Book)
                {
                    Book book = (Book)resource;
                    if (_comparisonerResources.CompareBooks(newBook, book))
                    {
                        throw new InvalidOperationException("Book Compare Exeption");
                    }
                }
            }

            RAMMemory.Library.Add(newBook);
            return true;
        }

        public bool DeleteBook(Guid id)
        {
            var resource = RAMMemory.Library.SingleOrDefault(r => r.Id == id);
            if (resource == null)
            {
                return false;
            }

            RAMMemory.Library.Remove(resource);
            return true;
        }

        public bool UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
