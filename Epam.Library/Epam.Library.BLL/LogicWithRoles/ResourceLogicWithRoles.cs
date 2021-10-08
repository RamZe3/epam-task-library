using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.SQLDAL;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class ResourceLogicWithRoles : IResourceLogic
    {
        private IResourceDAL resourceDAL;
        private UserRollProvider UserRollProvider;
        LogsSQLDAL LogsSQLDAL = new LogsSQLDAL();

        public ResourceLogicWithRoles(IResourceDAL resourceDAL, UserRollProvider userRollProvider)
        {
            this.resourceDAL = resourceDAL;
            UserRollProvider = userRollProvider;
        }

        public bool UpdateResourceStatus(Guid id, string status)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                      UserRollProvider.UserInRoleLibrarian())
            {
                LogsSQLDAL.AddLog(id, "Resource", UserRollProvider.user, "Update Resource Status");
                return resourceDAL.UpdateResourceStatus(id, status);
            }
                
            else
                throw new Exception();
        }
    }
}
