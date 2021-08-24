using Epam.Library.BLL;
using Epam.Library.BLL.Interfaces;
using Epam.Library.DAL.Interfaces;
using Epam.Library.RAMMemoryDAL;
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
        public IBookDAL bookDAL => new BookRAMDAL();
        public IPaperDAL paperDAL => new PaperRAMDAL();
        public IPatentDAL patentDAL => new PatentRAMDAL();

        public IInformationResourceLogic InformationResourceLogic => new InformationResourceLogic(InformationResourceDAL);
        public IBookLogic bookLogic => new BookLogic(bookDAL);
        public IPaperLogic paperLogic => new PaperLogic(paperDAL);
        public IPatentLogic patentLogic => new PatentLogic(patentDAL);
    }
}
