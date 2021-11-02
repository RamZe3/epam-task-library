using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces.Roles_system
{
    public interface IUsersLogic
    {
        bool AddUser(User user);
        bool DeleteUser(Guid id);
        bool AddRole(Guid id, string role);
        bool DeleteRole(Guid id, string role);
        bool DeleteAllRoleForUser(Guid id, string role);
        User GetUser(string UserName, string UserPass);
        List<User> GetUsers();
    }
}
