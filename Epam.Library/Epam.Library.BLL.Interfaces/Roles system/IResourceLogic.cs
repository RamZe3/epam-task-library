using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.Interfaces.Roles_system
{
    public interface IResourceLogic
    {
        bool UpdateResourceStatus(Guid  id, string status);
    }
}
