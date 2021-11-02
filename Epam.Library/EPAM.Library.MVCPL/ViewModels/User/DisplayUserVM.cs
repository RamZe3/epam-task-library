using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.User
{
    public class DisplayUserVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

    }
}