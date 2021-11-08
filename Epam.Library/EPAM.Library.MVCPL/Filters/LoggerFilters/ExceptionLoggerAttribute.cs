using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public Logger Logger = new Logger();
        public void OnException(ExceptionContext filterContext)
        {
            string userName;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                userName = filterContext.HttpContext.User.Identity.Name;
            }
            else
            {
                userName = "<guest>";
            }

            LogInfo logInfo = new LogInfo()
            {
                Id = Guid.NewGuid(),
                LogDescription = "Ex Message: " + filterContext.Exception.Message,
                ClassName = filterContext.RouteData.Values["controller"].ToString(),
                MethodName = filterContext.RouteData.Values["action"].ToString(),
                LayerName = filterContext.Exception.Source,
                UserName = userName,
                Date = DateTime.Now,
            };
            //filterContext.Exception.Data
            //logInfo.Id = Guid.NewGuid();
            //logInfo.LogDescription = "Test";

            Logger.SaveLog(logInfo);
        }
    }
}