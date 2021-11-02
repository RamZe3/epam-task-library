using Epam.Library.BLL.DateCheck;
using Epam.Library.BLL.Interfaces;
using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.SQLDAL;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class BooksLogicWithRoles : IBookLogic
    {
        private IBookLogic bookLogic;
        private UserRollProvider UserRollProvider;
        LogsSQLDAL LogsSQLDAL = new LogsSQLDAL();

        public BooksLogicWithRoles(IBookLogic bookLogic, UserRollProvider userRollProvider)
        {
            this.bookLogic = bookLogic; 
            UserRollProvider = userRollProvider;
        }

        public List<DataValidationError> AddBook(Book book)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                UserRollProvider.UserInRoleLibrarian()) {
                LogsSQLDAL.AddLog(book, UserRollProvider.user, "Add Book");
                return bookLogic.AddBook(book);
            }
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);
        }

        public bool DeleteBook(Guid id)
        {
            if (UserRollProvider.UserInRoleAdmin())
            {
                LogsSQLDAL.AddLog(id, "Book", UserRollProvider.user, "Delete Book");
                return bookLogic.DeleteBook(id);
            }
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);

        }

        public bool UpdateBook(Book book)
        {
            if (UserRollProvider.UserInRoleAdmin() ||
                UserRollProvider.UserInRoleLibrarian())
            {
                LogsSQLDAL.AddLog(book, UserRollProvider.user, "Update Book");
                return bookLogic.UpdateBook(book);
            }
            else
                throw new LackOfUserRightsException(UserRollProvider.user.Name);

        }
    }
}
