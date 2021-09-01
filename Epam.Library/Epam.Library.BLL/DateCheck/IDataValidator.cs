using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public interface IDataValidator
    {
        List<DataValidationError> IsBookCorrect(Book book);
        List<DataValidationError> IsPaperCorrect(Paper paper);
        List<DataValidationError> IsPatentCorrect(Patent patent);
    }
}
