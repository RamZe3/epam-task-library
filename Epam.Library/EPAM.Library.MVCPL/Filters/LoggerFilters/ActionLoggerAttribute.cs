using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Filters
{
    public class ActionLoggerAttribute : FilterAttribute, IActionFilter
    {
        public string Description { get; set; }
        public Logger Logger = new Logger();

        public ActionLoggerAttribute(string description)
        {
            Description = description;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogInfo logInfo = new LogInfo()
            {
                Id = Guid.NewGuid(),
                LogDescription = Description,
                ClassName = filterContext.RouteData.Values["controller"].ToString(),
                MethodName = filterContext.RouteData.Values["action"].ToString(),
                LayerName = filterContext.Controller.ToString(),
                UserName = filterContext.HttpContext.User.Identity.Name,
                Date = DateTime.Now,
            };

            Logger.SaveLog(logInfo);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
    }
}