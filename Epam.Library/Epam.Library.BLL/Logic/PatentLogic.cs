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
    public class PatentLogic : IPatentLogic
    {
        private IPatentDAL _patentDAL;
        private DataValidator _dataValidator;

        public PatentLogic(IPatentDAL patentDAL)
        {
            _dataValidator = new DataValidator();
            _patentDAL = patentDAL;
        }

        public void AddPatent(Patent patent)
        {
            if (!_dataValidator.IsPatentCorrect(patent))
            {
                return;
            }

            _patentDAL.AddPatent(patent);
        }

        public void DeletePatent(Guid guid)
        {
            _patentDAL.DeletePatent(guid);
        }
    }
}
