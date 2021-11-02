using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities.Exceptions
{
    public class LackOfUserRightsException : Exception
    {
        public override string Message { get; }
        public LackOfUserRightsException( string UserName)
        {
            Message = $"У {UserName} отсутсвуют права";
        }


    }
}
