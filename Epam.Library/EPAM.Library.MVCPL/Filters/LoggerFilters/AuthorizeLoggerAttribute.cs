using EPAM.Library.MVCPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.Filters
{
    public class AuthorizeLoggerAttribute : AuthorizeAttribute
    {
        public Logger Logger = new Logger();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            char[] punctuationMarks = { ' ', ',', '.', '!', '?', '\"', '\'', ':', ';', '(', ')' };
            string[] rolesMas = Roles.Split(punctuationMarks, StringSplitOptions.RemoveEmptyEntries);
            bool isUserInRole = false;
            foreach (var role in rolesMas)
            {
                if (filterContext.HttpContext.User.IsInRole(role))
                {
                    isUserInRole = true;
                    break;
                }
            }

            if (!isUserInRole)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    LogInfo logInfo = new LogInfo()
                    {
                        Id = Guid.NewGuid(),
                        LogDescription = String.Format("user wanted to access roles: {0}", Roles),
                        ClassName = filterContext.RouteData.Values["controller"].ToString(),
                        MethodName = filterContext.RouteData.Values["action"].ToString(),
                        LayerName = filterContext.Controller.ToString(),
                        UserName = filterContext.HttpContext.User.Identity.Name,
                        Date = DateTime.Now,
                    };

                    Logger.SaveLog(logInfo);
                }
            }

        }
    }
}