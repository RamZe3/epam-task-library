using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities.Interfaces
{
    public interface IHaveAuthors
    {
        List<Author> GetAuthors();
    }
}
