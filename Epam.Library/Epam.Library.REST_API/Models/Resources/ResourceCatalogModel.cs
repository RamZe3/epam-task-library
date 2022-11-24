using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Resources
{
    public class ResourceCatalogModel
    {
        public string Type { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
    }
}