using Epam.Library.BLL.Interfaces;
using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.SQLDAL;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class PapersLogicWithRoles : IPaperLogic
    {
        private LogsSQLDAL logsSQLDAL = new LogsSQLDAL();
        private IPaperLogic paperLogic;
        private UserRollProvider UserRollProvider;
        public PapersLogicWithRoles(IPaperLogic paperLogic, UserRollProvider userRollProvider)
        {
            this.paperLogic = paperLogic;
            UserRollProvider = userRollProvider;
        }

        public List<DataValidationError> AddPaper(Paper paper)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                   UserRollProvider.UserInRoleLibrarian())
            {
                logsSQLDAL.AddLog(paper, UserRollProvider.user, "Add Paper");
                return paperLogic.AddPaper(paper);
            }
                
            else
                throw new Exception();
        }

        public bool DeletePaper(Guid id)
        {
            if (UserRollProvider.UserInRoleAdmin())
            {
                logsSQLDAL.AddLog(id, "Paper", UserRollProvider.user, "Delete Paper");
                return paperLogic.DeletePaper(id);
            }
                
            else
                throw new Exception();
        }

        public bool UpdatePaper(Paper paper)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                   UserRollProvider.UserInRoleLibrarian())
            {
                logsSQLDAL.AddLog(paper, UserRollProvider.user, "Update Paper");
                return paperLogic.UpdatePaper(paper);
            }
                
            else
                throw new Exception();
            
        }
    }
}
