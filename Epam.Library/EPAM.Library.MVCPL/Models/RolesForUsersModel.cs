using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Models
{
    public class RolesForUsersModel
    {
        public List<SelectListItem> GetRolesForAdded()
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            roles.Add(new SelectListItem { Text = "Admin", Value = "admin" });
            roles.Add(new SelectListItem { Text = "Librarian", Value = "librarian", Selected = true });
            return roles;
        }

        public List<SelectListItem> GetRolesForDeleted(Guid id)
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            Epam.Library.Entities.User user = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers().Find(u => u.id == id);
            foreach (var role in user.Roles)
            {
                roles.Add(new SelectListItem { Text = role, Value = role });
            }

            if (roles.Count > 0)
            {
                roles[0].Selected = true;
            }
            return roles;
        }
    }
}