using BLL.Abstarct;
using BLL.Helpers;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    
    public class UsersController : Controller
    {
       // private Context context;
        private readonly IUserProvider _userProvider;
        public UsersController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        public ActionResult Index()// GET: Users
        { 
           return View(_userProvider.GetListUsers());
        }

        public ActionResult Create()
        {
            CreateUserViewModel model=new CreateUserViewModel();
            model.RoleId = 1;
            ViewBag.LisRoles = _userProvider.GetListBoxRoles();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var statusCreate = _userProvider.CreateUser(model);
                if (statusCreate == CreateUserStatus.Success)
                {

                    return RedirectToAction("Index", "Users");
                }
                if(statusCreate == CreateUserStatus.DuplicateEmail)
                    ModelState.AddModelError("", "Користувач з таким Email вже існує");
                if (statusCreate == CreateUserStatus.UserErrorCreate)
                    ModelState.AddModelError("", "Зверніться до адміністрації сайту");
            }
            //listRoles.Insert(0, new ListBoxItems() { Id = 0, Name = "" });
            ViewBag.LisRoles = _userProvider.GetListBoxRoles();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            UserEditViewModel model = _userProvider.EditUser(id);

            return View(model); 
        }
        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userProvider.EditUser(model);
            }
            return RedirectToAction("Index", "Users");
            
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            UserInfoViewModel model=_userProvider.DeleteUser(id);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(UserInfoViewModel model)
        {
            _userProvider.DeleteUser(model.ID);
            return RedirectToAction("Index", "Users");
        }
        public ActionResult AddRole()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddRole(AddRoleViewModel model)
        {
            _userProvider.AddRole(model);
            return RedirectToAction("Index", "Users");
        }
        
    }
}