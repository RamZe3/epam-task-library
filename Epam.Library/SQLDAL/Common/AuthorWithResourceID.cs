using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL.Common
{
    public class AuthorWithResourceID
    {
        public Guid ResourceId;
        public Author author;

        public AuthorWithResourceID(Guid resourceId, Author author)
        {
            ResourceId = resourceId;
            this.author = author;
        }
    }
}
