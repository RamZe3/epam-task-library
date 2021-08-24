using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class PatentRAMDAL : IPatentDAL
    {
        private ComparisonerResources _comparisonerResources = new ComparisonerResources();

        public void AddPatent(Patent newPatent)
        {
            foreach (var resource in RAMMemory.Library)
            {
                if (resource is Patent)
                {
                    Patent patent = (Patent)resource;
                    if (_comparisonerResources.ComparePatent(newPatent, patent))
                    {
                        throw new InvalidOperationException("Patent Compare Exeption");
                    }
                }
            }

            RAMMemory.Library.Add(newPatent);
        }

        public void DeletePatent(Guid id)
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
