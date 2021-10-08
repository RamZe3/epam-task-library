using Epam.Library.BLL;
using Epam.Library.BLL.Interfaces;
using Epam.Library.BLL.Interfaces.Roles_system;
using Epam.Library.BLL.LogicWithRoles;
using Epam.Library.DAL.Interfaces;
using Epam.Library.RAMMemoryDAL;
using Epam.Library.SQLDAL;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Dependencies
{
    public class DependenciesResolver
    {
        private static DependenciesResolver _instance;
        public static DependenciesResolver Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new DependenciesResolver();
                }

                return _instance;
            }
        }

        public IInformationResourceDAL InformationResourceDAL => new RAMMemory();
        public IBookDAL bookDAL => new BookSQLDAL();
        public IPaperDAL paperDAL => new PaperSQLDAL();
        public IPatentDAL patentDAL => new PatentSQLDAL();

        public IInformationResourceLogic InformationResourceLogic => new InformationResourceLogic(InformationResourceDAL);
        public IBookLogic bookLogic => new BookLogic(bookDAL);
        public IPaperLogic paperLogic => new PaperLogic(paperDAL);
        public IPatentLogic patentLogic => new PatentLogic(patentDAL);

        // Dependencies For role system

        public UserRollProvider UserRollProvider { set; get; }

        public IUserDAL userDAL => new UsersSQLDAL();
        public IResourceDAL resourceDAL => new ResourceSQLDAL();

        public IBookLogic booksLogicWithRoles => new BooksLogicWithRoles(bookLogic, UserRollProvider);
        public IPaperLogic papersLogicWithRoles => new PapersLogicWithRoles(paperLogic, UserRollProvider);
        public IPatentLogic patentsLogicWithRoles => new PatentsLogicWithRoles(patentLogic, UserRollProvider);
        public IResourceLogic iRLogicWithRoles => new ResourceLogicWithRoles(resourceDAL, UserRollProvider);
        public IInformationResourceLogic informationResourceLogic => new IRLogicWithRoles(informationResourceLogic, UserRollProvider);
    }
}
