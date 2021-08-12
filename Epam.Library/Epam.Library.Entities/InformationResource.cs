using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Entities
{
    public abstract class InformationResource
    {
        public Guid id { get; set; }
        public string name { get; set; }

        protected InformationResource(Guid id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override abstract string ToString();
    }
}
