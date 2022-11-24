using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Logic.Pages
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        private int TotalItems { get; set; }
        public int TotalPages 
        { 
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public List<T> GetItems<T>(List<T> collection)
        {
            TotalItems = collection.Count();

            return collection.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}