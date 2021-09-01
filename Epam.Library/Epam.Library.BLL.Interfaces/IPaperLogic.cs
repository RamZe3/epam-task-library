using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces
{
    public interface IPaperLogic
    {
        List<DataValidationError> AddPaper(Paper paper);
        bool DeletePaper(Guid guid);
    }
}
