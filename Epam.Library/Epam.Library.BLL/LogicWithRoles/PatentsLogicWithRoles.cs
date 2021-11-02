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
    public class PatentsLogicWithRoles : IPatentLogic
    {
        private IPatentLogic patentLogic;
        private UserRollProvider UserRollProvider;
        LogsSQLDAL LogsSQLDAL = new LogsSQLDAL();

        public PatentsLogicWithRoles(IPatentLogic patentLogic, UserRollProvider userRollProvider)
        {
            this.patentLogic = patentLogic;
            UserRollProvider = userRollProvider;
        }

        public List<DataValidationError> AddPatent(Patent patent)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                   UserRollProvider.UserInRoleLibrarian())
            {
                LogsSQLDAL.AddLog(patent, UserRollProvider.user, "Add Paper");
                return patentLogic.AddPatent(patent);
            }
                
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);
        }

        public bool DeletePatent(Guid id)
        {
            if (UserRollProvider.UserInRoleAdmin())
            {
                LogsSQLDAL.AddLog(id, "Patent", UserRollProvider.user, "Delete Patent");
                return patentLogic.DeletePatent(id);
            }
                
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);
        }

        public bool UpdatePatent(Patent patent)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                      UserRollProvider.UserInRoleLibrarian())
            {
                LogsSQLDAL.AddLog(patent, UserRollProvider.user, "Update Paper");
                return patentLogic.UpdatePatent(patent);
            }
                
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);
        }
    }
}
