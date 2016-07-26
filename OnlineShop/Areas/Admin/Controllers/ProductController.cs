using DAL;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libs;
using Model;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/
        [FilterAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [FilterAuthorize]
        public ActionResult GetListProduct(ProductIndexModel model)
        {
            var productBl = new ProductBL();
            model.ListProduct = productBl.GetAll();
            return View(model);
        }

        [FilterAuthorize]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        public JsonResult Save(Product model)
        {
            var response = new Response();
            try
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;
                var productBl = new ProductBL();
                if (model.Id > 0)
                {
                    productBl.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                    productBl.Insert(model);
                }
            }
            catch (Exception exception)
            {
                response.Code=SystemCode.Error;
                response.Message = exception.Message;
            }
            
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [FilterAuthorize]
        public JsonResult GetById(int id)
        {
            var response = new Response();
            try
            {
                var productBl = new ProductBL();
                var res = productBl.GetById(id);
                response.Data = res;
            }
            catch (Exception e)
            {
                response.Code = SystemCode.Error;
                response.Message = e.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
