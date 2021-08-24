using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public abstract class InformationResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Note { get; set; }

        protected InformationResource(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        protected InformationResource(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}
