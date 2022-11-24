using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Logic.SearchFilters
{
    public class ResourceSearchFilter
    {
        public List<string> Types { get; set; } = new List<string>();
        public int MinNumberOfPages { get; set; }
        public int MaxNumberOfPages { get; set; }
        public string Name { get; set; }

        public List<InformationResource> Filter(List<InformationResource> resources)
        {
            if (Name != null)
            {
                resources = resources.FindAll(r => NameFilter(r));
            }
            if (MinNumberOfPages != 0)
            {
                resources = resources.FindAll(r => MinNumberOfPagesFilter(r));
            }
            if (MaxNumberOfPages != 0)
            {
                resources = resources.FindAll(r => MaxNumberOfPagesFilter(r));
            }
            if (Types.Count != 0)
            {
                resources = resources.FindAll(r => TypesFilter(r));
            }
            return resources;
        }

        private bool NameFilter(InformationResource resource)
        {
            return resource.Name.Contains(Name) ? true : false;
        }

        private bool MinNumberOfPagesFilter(InformationResource resource)
        {
            return resource.NumberOfPages >= MinNumberOfPages ? true : false;
        }

        private bool MaxNumberOfPagesFilter(InformationResource resource)
        {
            return resource.NumberOfPages <= MaxNumberOfPages ? true : false;
        }

        private bool TypesFilter(InformationResource resource)
        {
            if (Types.Count == 0)
            {
                return true;
            }
            return Types.Contains(resource.GetType().Name) ? true : false;
        }
    }
}