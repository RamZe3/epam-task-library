using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class IRLogicWithRoles : InformationResourceLogic
    {
        private UserRollProvider UserRollProvider = new UserRollProvider();

        public IRLogicWithRoles(IInformationResourceDAL informationResourceDAL) : base(informationResourceDAL)
        {

        }

        public List<Book> FindBooksByAuthor(User user, string role ,Author author)
        {
            if (UserRollProvider.CheckRegister(user))
            {
                throw new Exception();
            }

            if (UserRollProvider.CheckUserInRoleRights(user, "user"))
            {
                return base.FindBooksByAuthor(author);
            }
            else
            {
                throw new Exception();
            }
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> FindResourcesByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetLibrary()
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<InformationResource>> GroupingResourceByYearOfPublication()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str)
        {
            throw new NotImplementedException();
        }
    }
}
