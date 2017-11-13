using BLL.Abstarct;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProvider _userProvider;
        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(_userProvider.Login(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.IsRememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login and password incorrect.");
                }
            }
            return View(model);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}