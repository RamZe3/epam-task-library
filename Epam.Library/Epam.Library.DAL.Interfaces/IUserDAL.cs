using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.DAL.Interfaces
{
    public interface IUserDAL
    {
        bool AddUser(User user);
        bool DeleteUser(Guid id);
        bool AddRole(Guid id, string role);
        bool DeleteRole(Guid id, string role);
        bool DeleteAllRoleForUser(Guid id, string role);
        User GetUser(string UserName, string UserPass);
    }
}
