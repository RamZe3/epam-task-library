using Epam.Library.BLL.LogicWithRoles;
using Epam.Library.Dependencies;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API
{
    public static class DependenciesResolverConfig
    {
        public static DependenciesResolver DependenciesResolver { get; private set; } = new DependenciesResolver();
        public static void Initialize()
        {
            User user = new User("Test123", "123");
            user.Roles.Add("admin");
            user.Roles.Add("librarian");
            user.Roles.Add("user");
            UserRollProvider UserRollProvider = new UserRollProvider(user);
            DependenciesResolver.UserRollProvider = UserRollProvider;
        }
    }
}