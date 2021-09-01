using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities.Exceptions
{
    public class DataValidationError
    {
        public string ErrorValue { get; }
        public string Message { get; }
        public DataValidationError(string message)
        {
            Message = message;
            ErrorValue = "";
        }

        public DataValidationError(string message, string errorValue)
        {
            Message = message;
            ErrorValue = errorValue;
        }

    }
}
