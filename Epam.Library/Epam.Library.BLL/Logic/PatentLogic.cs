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
    public class PatentLogic : IPatentLogic
    {
        private IPatentDAL _patentDAL;
        private DataValidator _dataValidator;

        public PatentLogic(IPatentDAL patentDAL)
        {
            _dataValidator = new DataValidator();
            _patentDAL = patentDAL;
        }

        public List<DataValidationError> AddPatent(Patent patent)
        {
            List<DataValidationError> dataValidationExceptions = _dataValidator.IsPatentCorrect(patent);
            if (dataValidationExceptions.Count != 0)
            {
                return dataValidationExceptions;
            }
            else
            {
                _patentDAL.AddPatent(patent);
                return dataValidationExceptions;
            }
        }

        public bool DeletePatent(Guid guid)
        {
            return _patentDAL.DeletePatent(guid);
        }

        public bool UpdatePatent(Patent patent)
        {
            return _patentDAL.UpdatePatent(patent);
        }
    }
}
