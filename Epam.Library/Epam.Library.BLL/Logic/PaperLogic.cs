using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
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

        public List<DataValidationError> AddPaper(Paper paper)
        {
            List<DataValidationError> dataValidationExceptions = _dataValidator.IsPaperCorrect(paper);

            if (dataValidationExceptions.Count != 0)
            {
                return dataValidationExceptions;
            }
            else
            {
                _paperDAL.AddPaper(paper);
                return dataValidationExceptions;
            }
        }

        public bool DeletePaper(Guid guid)
        {
           return _paperDAL.DeletePaper(guid);
        }

        public bool UpdatePaper(Paper paper)
        {
            return _paperDAL.UpdatePaper(paper);
        }
    }
}
