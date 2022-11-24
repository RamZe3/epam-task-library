using Epam.Library.Entities;
using Epam.Library.REST_API;
using Epam.Library.REST_API.UsersLogic;
using EPAM.Library.REST_API.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EPAM.Library.REST_API.Filters
{
    public class ActionLoggerAttribute : Attribute, IActionFilter
    {
        public string Description { get; set; }

        public bool AllowMultiple => false;

        public Logger Logger = new Logger();
        public JwtUtils JwtUtils = new JwtUtils();

        public ActionLoggerAttribute(string description)
        {
            Description = description;
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("token").FirstOrDefault();

            Guid? id = JwtUtils.ValidateToken(cookie["token"].Value);

            User user = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers().Find(x => x.id == id);

            if (user == null)
            {
                user = new User(name: "<guest>");
            }

            LogInfo logInfo = new LogInfo()
            {
                Id = Guid.NewGuid(),
                LogDescription = Description,
                ClassName = actionContext.ControllerContext.ControllerDescriptor.ControllerName,
                MethodName = actionContext.ActionDescriptor.ActionName,
                LayerName = actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                UserName = user.Name,
                Date = DateTime.Now,
            };

            Logger.SaveLog(logInfo);

            return continuation();
        }
    }
}