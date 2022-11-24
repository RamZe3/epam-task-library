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
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EPAM.Library.REST_API
{
    public class AuthorizeLoggerAttribute : Attribute, IActionFilter
    {
        public Logger Logger = new Logger();
        public JwtUtils JwtUtils = new JwtUtils();
        public string Roles { get; set; }

        public AuthorizeLoggerAttribute(string roles)
        {
            Roles = roles;
        }

        public AuthorizeLoggerAttribute()
        {
        }

        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("token").FirstOrDefault();

            if (cookie == null)
            {
                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }



            Guid? id = JwtUtils.ValidateToken(cookie["token"].Value);

            User user = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers().Find(x => x.id == id);

            if (user == null)
            {
                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                if (Roles == null)
                {
                    return continuation();
                }

                char[] punctuationMarks = { ' ', ',', '.', '!', '?', '\"', '\'', ':', ';', '(', ')' };
                string[] rolesMas = Roles.Split(punctuationMarks, StringSplitOptions.RemoveEmptyEntries);
                bool isUserInRole = false;
                foreach (var role in rolesMas)
                {
                    if (user.Roles.Contains(role))
                    {
                        isUserInRole = true;
                        break;
                    }
                }

                if (!user.Roles.Contains("externalClient"))
                {
                    isUserInRole = false;
                }

                if (!isUserInRole)
                {
                    LogInfo logInfo = new LogInfo()
                    {
                        Id = Guid.NewGuid(),
                        LogDescription = String.Format("user wanted to access roles: {0}", Roles),
                        ClassName = actionContext.ControllerContext.ControllerDescriptor.ControllerName,
                        MethodName = actionContext.ActionDescriptor.ActionName,
                        LayerName = actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                        UserName = user.Name,
                        Date = DateTime.Now,
                    };

                    Logger.SaveLog(logInfo);

                    return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
                }
                return continuation();
            }
        }
    }
}