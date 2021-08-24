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

        public void AddPaper(Paper newPaper)
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
        }

        public void DeletePaper(Guid id)
        {
            var resource = RAMMemory.Library.SingleOrDefault(r => r.Id == id);
            if (resource == null)
            {
                throw new InvalidOperationException("Delete Exception");
            }

            RAMMemory.Library.Remove(resource);
        }
    }
}
