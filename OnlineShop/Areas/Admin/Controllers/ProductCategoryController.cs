using DAL;
using Libs;
using Model;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        //
        // GET: /Admin/ProductCategory/

        [FilterAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [FilterAuthorize]
        public ActionResult GetListProductCategory()
        {
            var productCategoryBl = new ProductCategoryBL();
            return View(productCategoryBl.GetAll());
        }

        [FilterAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [FilterAuthorize]
        public JsonResult Save(ProductCategory input)
        {
            var response = new Response();
            try
            {
                var productCategoryBl = new ProductCategoryBL();

                input.ModifiedDate = DateTime.Now;
                input.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;
                if (input.Id > 0)
                {
                    productCategoryBl.Update(input);
                }
                else
                {
                    input.CreatedDate = DateTime.Now;
                    input.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                    productCategoryBl.Insert(input);
                }
            }
            catch (Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [FilterAuthorize]
        public JsonResult GetAll()
        {
            var response = new Response();
            try
            {
                var productCategoryBl = new ProductCategoryBL();
                var lst= productCategoryBl.GetAll();
                response.Data = lst;

            }
            catch (Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [FilterAuthorize]
        public JsonResult GetById(int id)
        {
            var response = new Response();
            try
            {
                var productCategoryBl = new ProductCategoryBL();
                response.Data = productCategoryBl.GetById(id);
            }
            catch (Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
