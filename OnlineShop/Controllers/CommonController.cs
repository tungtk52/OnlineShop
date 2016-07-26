using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public ActionResult Paging(Pager pager)
        {

            return View(pager);
        }

    }
}
