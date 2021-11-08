using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Filters
{
    public class AdminActionLoggerAttribute : FilterAttribute, IActionFilter
    {
        public Logger Logger = new Logger();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("admin"))
            {
                LogInfo logInfo = new LogInfo()
                {
                    Id = Guid.NewGuid(),
                    LogDescription = "action with admin rights",
                    ClassName = filterContext.RouteData.Values["controller"].ToString(),
                    MethodName = filterContext.RouteData.Values["action"].ToString(),
                    LayerName = filterContext.Controller.ToString(),
                    UserName = filterContext.HttpContext.User.Identity.Name,
                    Date = DateTime.Now,
                };

                Logger.SaveLog(logInfo);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
    }
}