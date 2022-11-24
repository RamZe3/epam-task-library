using Epam.Library.Entities;
using Epam.Library.REST_API.UsersLogic;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.REST_API;
using EPAM.Library.REST_API.Filters;
using EPAM.Library.REST_API.Models;
using EPAM.Library.REST_API.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Epam.Library.REST_API.Controllers
{
    [ExceptionLogger]
    public class UserController : ApiController
    {
        private HashGenerator hashGenerator = new HashGenerator();

        [ActionLogger(description: "Registration")]
        public HttpResponseMessage Registration([FromUri]CreateUserVM model)
        {
            JwtUtils jwtUtils = new JwtUtils();
            List <User> users = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers();

            if (ModelState.IsValid)
            {
                if (!users.Any(u => u.Name == model.Name))
                {
                    model.Password = hashGenerator.ToSHA512(model.Password);
                    User user = AutoMapperConfig.Mapper.Map<User>(model);
                    DependenciesResolverConfig.DependenciesResolver.UserRollProvider.AddUser(user);

                    var cookie = new CookieHeaderValue("token", jwtUtils.GenerateToken(user)); // имя куки - token
                    cookie.Expires = DateTimeOffset.Now.AddDays(7); // время действия куки - 1 день
                    cookie.Domain = Request.RequestUri.Host; // домен куки

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                    return response;


                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с этим логином уже существует");
                    var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionLogger(description: "Login")]
        public HttpResponseMessage Login([FromUri]LoginUserVM model)
        {
            JwtUtils jwtUtils = new JwtUtils();

            List<User> users = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers();

            if (ModelState.IsValid)
            {
                if (users.Any(u => u.Name == model.Name && u.Password == hashGenerator.ToSHA512(model.Password)))
                {
                    User user = users.Find(x => x.Name == model.Name && x.Password == hashGenerator.ToSHA512(model.Password));
                    //AuthTokenConfig.UserToken = jwtUtils.GenerateToken(user);

                    var cookie = new CookieHeaderValue("token", jwtUtils.GenerateToken(user)); // имя куки - token
                    cookie.Expires = DateTimeOffset.Now.AddDays(7); // время действия куки - 1 день
                    cookie.Domain = Request.RequestUri.Host; // домен куки

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                    return response;

                }
                else
                {
                    ModelState.AddModelError("", "Неверно указан логин или пароль");
                    var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Logout()
        {
            var cookie = new CookieHeaderValue("token", ""); // имя куки - token
            cookie.Expires = DateTimeOffset.Now.AddDays(7); // время действия куки - 1 день
            cookie.Domain = Request.RequestUri.Host; // домен куки

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;
        }

        public IHttpActionResult AddRole(Guid Id, string Role)
        {
            DependenciesResolverConfig.DependenciesResolver.UserRollProvider.AddRole(Id, Role);

            return Ok();
        }

        public IHttpActionResult DeleteRole(Guid Id, string Role)
        {
            DependenciesResolverConfig.DependenciesResolver.UserRollProvider.DeleteRole(Id, Role);

            return Ok();
        }

        [AuthorizeLogger]
        public IHttpActionResult GetToken()
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("token").FirstOrDefault();
            if (cookie == null)
            {
                return NotFound();
            }
            return Ok(cookie["token"].Value);
            //if (AuthTokenConfig.UserToken == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    return Ok(AuthTokenConfig.UserToken);
            //}
        }
    }
}
