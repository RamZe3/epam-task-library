using Epam.Library.BLL.Interfaces;
using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.BLL.LogicWithRoles
{
    public class IRLogicWithRoles : IInformationResourceLogic
    {
        //TODO IIRLogicWithRoles
        IInformationResourceLogic InformationResourceLogic;
        private UserRollProvider UserRollProvider;

        public IRLogicWithRoles(IInformationResourceLogic informationResourceLogic, UserRollProvider userRollProvider)
        {
            InformationResourceLogic = informationResourceLogic;
            UserRollProvider = userRollProvider;
        }

        public List<Book> FindBooksByAuthor(Author author)
        {
            return InformationResourceLogic.FindBooksByAuthor(author);
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            return InformationResourceLogic.FindPatentsAndBooksByAuthor(author);
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            return InformationResourceLogic.FindPatentsByAuthor(author);
        }

        public List<InformationResource> FindResourcesByName(string name)
        {
            return InformationResourceLogic.FindResourcesByName(name);
        }

        public List<InformationResource> GetLibrary()
        {
            return InformationResourceLogic.GetLibrary();
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            return InformationResourceLogic.GetSortedLibraryByYearOfPublishing(reverse);
        }

        public Dictionary<int, List<InformationResource>> GroupingResourceByYearOfPublication()
        {
            return InformationResourceLogic.GroupingResourceByYearOfPublication();
        }

        public Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str)
        {
            return InformationResourceLogic.SmartBookSearchByPublisher(str);
        }

        
    }
}
