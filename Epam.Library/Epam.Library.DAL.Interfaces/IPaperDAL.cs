using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.DAL.Interfaces
{
    public interface IPaperDAL
    {
        bool AddPaper(Paper paper);
        bool UpdatePaper(Paper paper);
        bool DeletePaper(Guid id);
    }
}
