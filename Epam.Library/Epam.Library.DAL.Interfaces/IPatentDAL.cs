using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.DAL.Interfaces
{
    public interface IPatentDAL
    {
        void AddPatent(Patent patent);
        void DeletePatent(Guid id);
    }
}
