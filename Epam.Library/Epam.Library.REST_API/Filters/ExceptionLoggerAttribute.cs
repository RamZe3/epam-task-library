using Epam.Library.Entities;
using Epam.Library.REST_API;
using Epam.Library.REST_API.UsersLogic;
using EPAM.Library.REST_API.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace EPAM.Library.MVCPL.Filters
{
    [ExceptionLogger]
    public class ExceptionLoggerAttribute : Attribute, IExceptionFilter
    {
        public Logger Logger = new Logger();
        public JwtUtils JwtUtils = new JwtUtils();

        public bool AllowMultiple => throw new NotImplementedException();

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            CookieHeaderValue cookie = actionExecutedContext.Request.Headers.GetCookies("token").FirstOrDefault();

            Guid? id = JwtUtils.ValidateToken(cookie["token"].Value);

            User user = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers().Find(x => x.id == id);

            if (user == null)
            {
                user = new User(name: "<guest>");
            }

            LogInfo logInfo = new LogInfo()
            {
                Id = Guid.NewGuid(),
                LogDescription = actionExecutedContext.Exception.Message,
                ClassName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                MethodName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                LayerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                UserName = user.Name,
                Date = DateTime.Now,
            };

            Logger.SaveLog(logInfo);

            return Task.FromResult<HttpResponseMessage>(
                       actionExecutedContext.ActionContext.Request.CreateResponse(HttpStatusCode.InternalServerError)); ;
        }
    }
}