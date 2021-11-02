using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.ViewModels.User
{
    public class DeleteRoleVM
    {
        public Guid Id { get; set; }

        //[Required]
        public List<SelectListItem> Roles =>  new RolesForUsersModel().GetRolesForDeleted(Id);
        public string Role { get; set; }
    }
}