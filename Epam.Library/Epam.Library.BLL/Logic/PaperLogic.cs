using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL
{
    public class PaperLogic : IPaperLogic
    {
        private IPaperDAL _paperDAL;
        private DataValidator _dataValidator;

        public PaperLogic(IPaperDAL paperDAL)
        {
            _dataValidator = new DataValidator();
            _paperDAL = paperDAL;
        }

        public void AddPaper(Paper paper)
        {
            if (!_dataValidator.IsPaperCorrect(paper))
            {
                return;
            }

            _paperDAL.AddPaper(paper);
        }

        public void DeletePaper(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
