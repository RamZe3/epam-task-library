using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces
{
    public interface IPaperLogic
    {
        void AddPaper(Paper paper);
        void DeletePaper(Guid guid);
    }
}
