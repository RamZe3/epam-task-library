using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.ViewModels.User
{
    public class AddRoleVM
    {
        public Guid Id { get; set; }

        //[Required]
        public List<SelectListItem> Roles => new RolesForUsersModel().GetRolesForAdded();
        public string Role { get; set; }

        public AddRoleVM()
        {
            Roles.Add(new SelectListItem { Text = "Admin", Value = "admin"});
            Roles.Add(new SelectListItem { Text = "Librarian", Value = "librarian", Selected = true});
        }
    }
}