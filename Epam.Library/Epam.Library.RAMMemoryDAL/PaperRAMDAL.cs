using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class PaperRAMDAL : IPaperDAL
    {
        private ComparisonerResources _comparisonerResources = new ComparisonerResources();

        public bool AddPaper(Paper newPaper)
        {
            foreach (var resource in RAMMemory.Library)
            {
                if (resource is Paper)
                {
                    Paper paper = (Paper)resource;
                    if (_comparisonerResources.ComparePaper(newPaper, paper))
                    {
                        throw new InvalidOperationException("Paper Compare Exeption");
                    }
                }
            }

            RAMMemory.Library.Add(newPaper);
            return true;
        }

        public bool DeletePaper(Guid id)
        {
            var resource = RAMMemory.Library.SingleOrDefault(r => r.Id == id);
            if (resource == null)
            {
                return false;
            }

            RAMMemory.Library.Remove(resource);
            return true;
        }

        public bool UpdatePaper(Paper paper)
        {
            throw new NotImplementedException();
        }
    }
}
