using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class UserRollProvider
    {
        public bool CheckRegister(User user)
        {
            if (user.IsRegister)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //RolesCheckers

        public bool UserInRoleAdmin(User user)
        {
            return user.Roles.Contains("admin");
        }

        public bool UserInRoleLibrarian(User user)
        {
            return user.Roles.Contains("admin") ||
               user.Roles.Contains("librarian");
        }

        public bool UserInRoleUser(User user)
        {
            return user.Roles.Contains("admin") ||
               user.Roles.Contains("librarian") ||
               user.Roles.Contains("user");
        }

        //Rights checker
        public bool CheckUserInRoleRights(User user, string role)
        {
            switch (role)
            {
                case "librarian":
                     return UserInRoleLibrarian(user);
                case "user":
                    return UserInRoleUser(user);
                case "admin":
                    return UserInRoleAdmin(user);
                default:
                    return false;
            }
        }
    }
}
