using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Library.BLL.DateCheck;
using Epam.Library.Entities.Exceptions;

namespace Epam.Library.BLL
{
    public class BookLogic : IBookLogic
    {
        private DataValidator _dataValidator;
        private IBookDAL _bookDAL;

        public BookLogic(IBookDAL bookDAL)
        {
            _bookDAL = bookDAL;
            _dataValidator = new DataValidator();
        }

        public List<DataValidationError> AddBook(Book book)
        {
            List<DataValidationError> dataValidationExceptions = _dataValidator.IsBookCorrect(book);
            if (dataValidationExceptions.Count != 0)
            {
                return dataValidationExceptions;
            }
            else
            {
                    _bookDAL.AddBook(book);
                return dataValidationExceptions;
            }
        }

        public bool DeleteBook(Guid guid)
        {
            return _bookDAL.DeleteBook(guid);
        }

        public bool UpdateBook(Book book)
        {
            return _bookDAL.UpdateBook(book);
        }
    }
}
