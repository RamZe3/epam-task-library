using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class UserRollProvider : IUsersLogic
    {
        public User user;
        private UsersSQLDAL UsersSQLDAL = new UsersSQLDAL();

        public UserRollProvider(User user)
        {
            this.user = GetUserFromDataBase(user.Name, user.Password);
        }

        public UserRollProvider()
        {
        }

        public User GetUserFromDataBase(string name, string pass)
        {
            User userFromBase = UsersSQLDAL.GetUser(name, pass);
            return userFromBase;
        }

        public bool UserInRoleUser()
        {
            return user.Roles.Contains("user");
        }

        public bool UserInRoleAdmin()
        {
            return user.Roles.Contains("admin");
        }

        public bool UserInRoleLibrarian()
        {
            return user.Roles.Contains("librarian");
        }

        public bool AddUser(User user)
        {
            user.Roles.Add("user");
             return UsersSQLDAL.AddUser(user);
        }

        public bool DeleteUser(Guid id)
        {
            if (UserInRoleAdmin())
                return UsersSQLDAL.DeleteUser(id);
            else
                throw new LackOfUserRightsException(user.Name);
        }

        public bool AddRole(Guid id, string role)
        {
            if (UserInRoleAdmin())
                return UsersSQLDAL.AddRole(id, role);
            else
                throw new LackOfUserRightsException(user.Name);
        }

        public bool DeleteRole(Guid id, string role)
        {
            if (UserInRoleAdmin())
                return UsersSQLDAL.DeleteRole(id, role);
            else
                throw new LackOfUserRightsException(user.Name);
        }

        public bool DeleteAllRoleForUser(Guid id, string role)
        {
            if (UserInRoleAdmin())
                return UsersSQLDAL.DeleteAllRoleForUser(id, role);
            else
                throw new LackOfUserRightsException(user.Name);
        }

        public User GetUser(string UserName, string UserPass)
        {
            if (UserInRoleAdmin())
                return UsersSQLDAL.GetUser(UserName, UserPass);
            else
                throw new LackOfUserRightsException(user.Name);
        }

        public List<User> GetUsers()
        {
            return UsersSQLDAL.GetUsers();
        }
    }
}
