using System.Web;
using System.Web.Mvc;

namespace Epam.Library.ASP.NET_WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
