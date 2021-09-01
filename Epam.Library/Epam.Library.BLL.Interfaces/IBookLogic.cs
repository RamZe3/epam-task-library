using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces
{
    public interface IBookLogic
    {
        List<DataValidationError> AddBook(Book book);
        bool DeleteBook(Guid guid);
    }
}
