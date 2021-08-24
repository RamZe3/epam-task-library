using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.DateCheck
{
    public interface IDataValidator
    {
        bool IsBookCorrect(Book book);
        bool IsPaperCorrect(Paper paper);
        bool IsPatentCorrect(Patent patent);
    }
}
