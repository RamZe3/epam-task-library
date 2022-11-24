using Epam.Library.BLL.LogicWithRoles;
using Epam.Library.Dependencies;
using Epam.Library.Entities;
using EPAM.Library.MVCPL.Filters;
using EPAM.Library.MVCPL.Models;
using EPAM.Library.MVCPL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EPAM.Library.MVCPL.Controllers
{
    [ExceptionLogger]
    [AuthorizeLogger(Roles = "admin")]
    public class UsersController : Controller
    {
        // GET: Users
        private HashGenerator hashGenerator = new HashGenerator();
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        
        public ActionResult Registration(CreateUserVM model)
        {

            List<User> users = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers();

            if (ModelState.IsValid)
            {
                if (!users.Any(u => u.Name == model.Name))
                {
                    model.Password = hashGenerator.ToSHA512(model.Password);
                    DependenciesResolverConfig.DependenciesResolver.UserRollProvider.AddUser(AutoMapperConfig.Mapper.Map<User>(model));
                    FormsAuthentication.SetAuthCookie(model.Name, createPersistentCookie: true);
                    return RedirectToAction(nameof(SuccessfulRegistration));

                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с этим логином уже существует");
                    return View(model);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [ActionLogger("registration")]
        public ActionResult SuccessfulRegistration()
        {

            return RedirectToAction(nameof(Index), "Main");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {
            List<User> users = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers();

            if (ModelState.IsValid)
            {
                if (users.Any(u => u.Name == model.Name && u.Password == hashGenerator.ToSHA512(model.Password)))
                {
                    FormsAuthentication.SetAuthCookie(model.Name, createPersistentCookie: true);
                    return RedirectToAction(nameof(SuccessfulLogin));
                    
                }
                else
                {
                    ModelState.AddModelError("", "Неверно указан логин или пароль");
                    return View(model);
                }    
            }

            return View(model);
        }

        [AllowAnonymous]
        [ActionLogger("login")]
        public ActionResult SuccessfulLogin()
        {
            return RedirectToAction(nameof(Index), "Main");
        }

        [OverrideAuthorization]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(Index), "Main");
        }

        [ActionLogger("action with admin roles")]
        public ActionResult GetUsers()
        {
            List<User> users = DependenciesResolverConfig.DependenciesResolver.UserRollProvider.GetUsers();
            List<DisplayUserVM> displayUserVMs = AutoMapperConfig.Mapper.Map<List<DisplayUserVM>>(users);
            return View(displayUserVMs);
        }

        public ActionResult AddRole(Guid id)
        {
            AddRoleVM addRoleVM = new AddRoleVM() { Id = id };
            addRoleVM.Roles = new RolesForUsersModel().GetRolesForAdded();
            return View(addRoleVM);
        }

        [ActionLogger("action with admin roles")]
        [HttpPost]
        public ActionResult AddRole(AddRoleVM model)
        {
            if (ModelState.IsValid)
            {
                DependenciesResolverConfig.DependenciesResolver.UserRollProvider.AddRole(model.Id, model.Role);
                //return View(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult DeleteRole(Guid id)
        {
            DeleteRoleVM deleteRoleVM = new DeleteRoleVM() {Id = id };
            deleteRoleVM.Roles = new RolesForUsersModel().GetRolesForDeleted(id);
            return View(deleteRoleVM);
        }

        [HttpPost]
        public ActionResult DeleteRole(DeleteRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "user")
                {
                    ModelState.AddModelError("", "Роль user удалить нельзя");
                    return View(model);
                }

                DependenciesResolverConfig.DependenciesResolver.UserRollProvider.DeleteRole(model.Id, model.Role);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult View1()
        {

            return View();
        }
    }
}