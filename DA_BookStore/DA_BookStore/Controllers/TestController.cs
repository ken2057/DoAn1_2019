using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_BookStore.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Test()
        {
            using (var db = new Models.BookStore())
            {
                ViewBag.DsSach = db.SACHes.ToList();
            }
            return View();
        }
    }
}