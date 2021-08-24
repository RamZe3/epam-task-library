using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Library.BLL.DateCheck;

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

        public void AddBook(Book book)
        {
            if (!_dataValidator.IsBookCorrect(book))
            {
                return;
            }

            _bookDAL.AddBook(book);
        }

        public void DeleteBook(Guid guid)
        {
            _bookDAL.DeleteBook(guid);
        }
    }
}
