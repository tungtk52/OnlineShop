using DAL;
using Libs;
using Model;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController:Controller
    {
        //
        // GET: /Admin/User/

        [FilterAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginModel model)
        {
            var response = new Response();

            var obj=new OnlineShop.Areas.Admin.Membership.CustomMembershipProvider();
            if (obj.ValidateUser(model.UserName,model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                Session.Add(ConfigurationManager.AppSettings["SessionUser"], model.UserName);
                return Redirect("/Admin/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Add(ConfigurationManager.AppSettings["SessionUser"], null);
            return RedirectToAction("LogOn","User");
        }

        [FilterAuthorize]
        public ActionResult GetListUser()
        {
            
            var userBl = new UserBL();
            return View(userBl.GetAll());
        }

        [FilterAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [FilterAuthorize]
        public ActionResult Edit()
        {
            return View();
        }

        [FilterAuthorize]
        public ActionResult Save(User user)
        {
            var response = new Response();
            try
            {
                var userBl = new UserBL();
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;
                if (user.Id>0)
                {
                    userBl.Update(user);
                }
                else
                {
                    user.CreatedDate = DateTime.Now;
                    user.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                    userBl.Insert(user);
                }
            }
            catch(Exception ex)
            {
                response.Code = SystemCode.Error;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [FilterAuthorize]
        public ActionResult GetById(int id)
        {
            var response = new Response();
            try
            {
                var userBl = new UserBL();
                response.Data=userBl.GetById(id);
            }
            catch (Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [FilterAuthorize]
        public ActionResult Delete(int id)
        {
            var response = new Response();
            try {
                var userBl = new UserBL();
                userBl.Delete(id);
            }
            catch(Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
